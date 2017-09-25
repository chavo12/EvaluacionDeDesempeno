Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports AugEntities
Imports AugHelper
Imports System.Web

Namespace AugMapper.Common

    Public MustInherit Class AbstractMapper
        Implements IMapper

        Private Shared _CadenaConexion As String = String.Empty

        Protected MustOverride Function DoLoad(ByVal reader As SqlDataReader) As Object
        Protected MustOverride Function ExecuteChildEntities() As Boolean
        Protected MustOverride Sub DoLoadChildEntities(ByVal entidad As AugEntities.Common.Entity)
        Protected MustOverride Function GetList() As IList

        Protected MustOverride Function Insert(ByVal entidad As AugEntities.Common.Entity, Optional ByVal idEntidadFK As Integer = 0) As Integer Implements IMapper.Insert
        Protected MustOverride Sub Update(ByVal entidad As AugEntities.Common.Entity, Optional ByVal idEntidadFK As Integer = 0) Implements IMapper.Update

        Private TablaFK As New Hashtable
        Private Shared _LstConexion As New List(Of AugSqlConnection)
        Private _command As SqlCommand = Nothing
        Protected _isStoreProcedure As Boolean = True
        Private Const IDUNICO As String = "idUnico"

        Private _rdCmd As New SqlCommand()
        Private _rdId As SqlDataReader

        Private Shared _TransacActiva As Boolean
        Private Shared _Transaccion As SqlTransaction

        Public Sub New()

        End Sub

#Region "Propiedades"

        Private ReadOnly Property ConexionHabilitada() As AugSqlConnection
            Get
                ' Si estoy en una transaccion devuelvo la conexion afectada
                If AugSqlConnection.ConexionTransaccion IsNot Nothing Then Return AugSqlConnection.ConexionTransaccion

                Dim retConex As AugSqlConnection = Nothing

                For Each oSqlCon As AugSqlConnection In _LstConexion

                    If oSqlCon IsNot Nothing AndAlso Not oSqlCon.Lock Then retConex = oSqlCon

                Next

                If retConex Is Nothing Then
                    _LstConexion.Add(New AugSqlConnection(New SqlConnection(CadenaConexion)))
                    retConex = _LstConexion(_LstConexion.Count - 1)
                End If

                Return retConex
            End Get
        End Property


        Public Sub ClearConexionList()

            If _LstConexion IsNot Nothing Then
                _CadenaConexion = String.Empty
                _LstConexion.Clear()
            End If


        End Sub


        Protected ReadOnly Property CadenaConexion() As String
            Get
                If _CadenaConexion = String.Empty Then

                    If Ambiente.EsClienteEscritorio Then
                        ' Tomo la configuracion de gestion.ini
                        _CadenaConexion = Ambiente.ConexionEscritorio

                    ElseIf Ambiente.EsWebService Then
                        ' Tomo configuracion de web.config
                        _CadenaConexion = Ambiente.ConexionWebSQL

                    ElseIf Ambiente.EsComparador Then
                        ' Tomo configuracion de web.config
                        _CadenaConexion = Ambiente.ConexionComparador

                    ElseIf Ambiente.EsServicio Then
                        ' Tomo configuracion de web.config
                        _CadenaConexion = Ambiente.ConexionServicio

                    End If
                End If
                Return _CadenaConexion
            End Get
        End Property

        Protected Property CommandName() As String
            Get
                If _command IsNot Nothing Then
                    Return _command.CommandText
                Else
                    Return ""
                End If
            End Get
            Set(ByVal value As String)
                If _command Is Nothing Then
                    _command = New SqlCommand()
                    If TransaccionActiva Then _command.Transaction = AugSqlConnection.Transaction
                End If
                _command.CommandText = value
                _command.Parameters.Clear()
            End Set
        End Property

        Protected ReadOnly Property TransaccionActiva() As Boolean
            Get
                Return _TransacActiva
            End Get
        End Property

        'Protected Property Transaction() As SqlTransaction
        '    Get
        '        Return _Transaccion
        '    End Get
        '    Set(ByVal value As SqlTransaction)
        '        _Transaccion = value
        '    End Set
        'End Property

#End Region

        Protected Sub agregarReferencia(ByVal entidad As Entity, ByVal clave As String, ByVal valor As Integer)
            If Not TablaFK.ContainsKey(entidad.id) Then TablaFK.Add(entidad.id, New Hashtable)

            Dim TablaInt As Hashtable = DirectCast(TablaFK(entidad.id), Hashtable)

            If TablaInt.ContainsKey(clave) Then TablaInt.Remove(clave)
            TablaInt.Add(clave, valor)
        End Sub

        Protected Function valorReferencia(ByVal entidad As Entity, ByVal clave As String) As Integer
            Dim returnValue As Integer = 0
            If TablaFK.ContainsKey(entidad.id) Then
                Dim TablaInt As Hashtable = DirectCast(TablaFK(entidad.id), Hashtable)
                If TablaInt.ContainsKey(clave) Then returnValue = CInt(TablaInt(clave))
            End If
            Return returnValue
        End Function


        Private Sub Conectar(Optional ByVal ComprobarStore As Boolean = True)
            Try

                If _command IsNot Nothing Then

                    _command.Connection = ConexionHabilitada.Open().Conexion

                    If ComprobarStore Then
                        If _isStoreProcedure Then
                            _command.CommandType = CommandType.StoredProcedure
                        Else
                            _command.CommandType = CommandType.Text
                        End If
                    End If
                End If
            Catch ext As AugTechnicalException
                Throw
            Catch ex As Exception
                Throw New AugTechnicalException("No se pudo conectar a la base (Conectar) : ", ex)
            End Try
        End Sub

        Private Sub Desconectar(Optional ByVal oCommand As SqlCommand = Nothing)
            Try
                If oCommand Is Nothing Then oCommand = _command
                Dim oAugSql As AugSqlConnection = AugSqlConnection.GetInstance(_LstConexion, oCommand.Connection)
                If oAugSql IsNot Nothing Then oAugSql.Close()
            Catch ext As AugTechnicalException
                Throw
            Catch ex As Exception
                Throw New AugTechnicalException("No se pudo desconectar de la base (Desconectar) : ", ex)
            End Try
        End Sub

#Region "Transaction "

        Protected Sub BeginTransaction() Implements IMapper.BeginTransaction

            Try
                If _TransacActiva Then Exit Sub

                Dim oAugSql As AugSqlConnection

                If _command IsNot Nothing Then
                    oAugSql = AugSqlConnection.GetInstance(_LstConexion, _command.Connection)
                Else : oAugSql = ConexionHabilitada.Open()
                End If

                oAugSql.BeginTransaction()

                If _command IsNot Nothing Then _command.Transaction = AugSqlConnection.Transaction

                _TransacActiva = True

            Catch ext As AugTechnicalException
                Throw
            Catch ex As Exception
                Throw New AugTechnicalException("No se pudo iniciar la transaccion", ex)

            End Try

        End Sub


        Protected Sub CommitTransaction() Implements IMapper.CommitTransaction
            Try

                If Not _TransacActiva Then Exit Sub

                AugSqlConnection.Transaction.Commit()

                _TransacActiva = False

            Catch ext As AugTechnicalException
                Throw
            Catch ex As Exception
                Throw New AugTechnicalException("No se pudo ejecutar commit en la transaccion", ex)

            End Try
        End Sub

        Protected Sub RollbackTransaction() Implements IMapper.RollbackTransaction
            Try

                If Not _TransacActiva Then Exit Sub

                AugSqlConnection.Transaction.Rollback()

                _TransacActiva = False

            Catch ext As AugTechnicalException
                Throw
            Catch ex As Exception
                Throw New AugTechnicalException("No se pudo ejecutar rollback en la transaccion", ex)

            End Try
        End Sub

#End Region

        Protected Function ExecuteScalar() As Integer
            Dim retInt As Integer = -1
            Try
                Conectar()
                retInt = Convert.ToInt32(_command.ExecuteScalar)

            Catch ext As AugTechnicalException
                Throw
            Catch ex As Exception
                Throw New AugTechnicalException("No se pudo ejecutar el comando : " & _command.CommandText, ex)
            Finally
                Desconectar()
            End Try
            Return retInt
        End Function

        Protected Function ExecuteStrScalar() As String
            Dim retStr As String = String.Empty
            Try
                Conectar()
                retStr = Convert.ToString(_command.ExecuteScalar)

            Catch ext As AugTechnicalException
                Throw
            Catch ex As Exception
                Throw New AugTechnicalException("No se pudo ejecutar el comando : " & _command.CommandText, ex)
            Finally
                Desconectar()
            End Try
            Return retStr
        End Function

        Protected Function ExecuteIntScalar() As Integer
            Dim retInt As Integer = -1

            _command.Parameters.Add("@Return_Value", SqlDbType.Int)
            _command.Parameters("@Return_Value").Direction = ParameterDirection.ReturnValue

            ExecuteNonQuery()

            retInt = Convert.ToInt32(_command.Parameters("@Return_Value").Value)

            Return retInt
        End Function



        Private Function ExecuteReader() As SqlDataReader
            Dim retObj As SqlDataReader = Nothing
            Try
                Conectar()
                retObj = _command.ExecuteReader()

            Catch ext As AugTechnicalException
                Throw
            Catch ex As Exception
                Throw New AugTechnicalException("No se pudo ejecutar el comando : " & _command.CommandText, ex)
            End Try
            Return retObj
        End Function

        Protected Sub ExecuteNonQuery()
            Try

                Conectar()

                _command.ExecuteNonQuery()

            Catch ext As AugTechnicalException
                Throw
            Catch ex As Exception
                Throw New AugTechnicalException("No se pudo ejecutar el comando : " & _command.CommandText, ex)
            Finally
                Desconectar()
            End Try

        End Sub

        Public Overridable Function Save(ByVal entidad As AugEntities.Common.Entity, Optional ByVal idEntidadFK As Integer = 0) As Integer
            If entidad IsNot Nothing Then

                Try

                    If entidad.id = 0 Then
                        entidad.id = Insert(entidad, idEntidadFK)
                    Else
                        Update(entidad, idEntidadFK)
                    End If

                    Return entidad.id

                Catch ex As AugTechnicalException
                    If ex.Entity Is Nothing Then ex.Entity = entidad
                    Throw ex

                Catch ex As Exception
                    Throw New AugBusinessException("No se pudo salvar en la capa de datos", ex)

                End Try

            End If
            Return -1
        End Function

        Protected Sub ExisteIdUnico(ByRef entidad As Entity, Optional TablaMapeada As String = "") Implements IMapper.ExisteIdUnico
            'Recibe el idUnico de una Tabla
            'Si existe ese idUnico lo setea en la entidad
            'Sino existe se deja el que trae (=0)
            ' Se toma la variable TablaMapeada desde una propiedad que tiene la entidad

            If TablaMapeada = "" Then

                Dim augTabla As AugTabla() = DirectCast(entidad.GetType.GetCustomAttributes(GetType(AugTabla), False), AugTabla())
                TablaMapeada = CStr(IIf(augTabla.Length > 0, augTabla(0).Tabla, String.Empty))

            End If


            If (Not entidad.idUnico.Equals(Guid.Empty)) AndAlso TablaMapeada <> "" Then

                Try

                    _rdCmd.Connection = ConexionHabilitada.Open.Conexion
                    _rdCmd.CommandType = CommandType.StoredProcedure
                    If TransaccionActiva Then _rdCmd.Transaction = AugSqlConnection.Transaction
                    _rdCmd.Parameters.Clear()
                    _rdCmd.Parameters.Add("@tabla", SqlDbType.VarChar).Value = TablaMapeada
                    _rdCmd.Parameters.Add("@idUnico", SqlDbType.UniqueIdentifier).Value = entidad.idUnico
                    _rdCmd.CommandText = "Aug_GetPrimaryKeyValue"

                    _rdId = _rdCmd.ExecuteReader

                    If _rdId.Read AndAlso _rdId("PrimaryKeyValue") IsNot DBNull.Value Then

                        entidad.id = Convert.ToInt32(_rdId("PrimaryKeyValue"))

                    End If

                    If _rdId IsNot Nothing AndAlso Not _rdId.IsClosed Then
                        _rdId.Close()
                    End If

                Catch ext As AugTechnicalException
                    Throw
                Catch ex As Exception
                    Throw New AugTechnicalException("No se pudo ejecutar el comando : " & _rdCmd.CommandText, ex)
                Finally
                    Desconectar(_rdCmd)
                End Try
            End If

        End Sub

        Protected Function GetOne() As Object
            Dim returnObject As Object = Nothing
            Dim rd As SqlDataReader = Nothing

            Try
                rd = ExecuteReader()

                If rd.Read() Then
                    returnObject = DoLoad(rd)

                End If

            Catch ext As AugTechnicalException
                Throw
            Catch ex As Exception
                Throw New AugTechnicalException("No se pudo ejecutar leer desde Reader (GetOne) : ", ex)

            Finally
                If rd IsNot Nothing Then
                    If Not rd.IsClosed Then
                        rd.Close()
                    End If
                    rd.Dispose()
                End If
                Desconectar()
            End Try

            If returnObject IsNot Nothing AndAlso ExecuteChildEntities() Then
                DoLoadChildEntities(DirectCast(returnObject, Entity))
                If TablaFK IsNot Nothing Then TablaFK.Clear()
            End If

            Return returnObject
        End Function


        Protected Function GetEntityList() As IList
            Dim returnObject As IList = Nothing
            Dim rd As SqlDataReader = Nothing

            Try
                rd = ExecuteReader()

                If rd.HasRows Then
                    returnObject = LoadAll(rd)
                Else
                    returnObject = GetList()
                End If

            Catch ext As AugTechnicalException
                Throw
            Catch ex As Exception
                Throw New AugTechnicalException("No se pudo ejecutar leer desde Reader (GetEntityList) : ", ex)

            Finally
                If rd IsNot Nothing Then
                    If Not rd.IsClosed Then
                        rd.Close()
                    End If
                    rd.Dispose()
                End If
                Desconectar()
            End Try

            If ExecuteChildEntities() Then
                LoadAllChildEntities(returnObject)
                If TablaFK IsNot Nothing Then TablaFK.Clear()
            End If

            Return returnObject
        End Function

        Protected Function GetListOfIntegers(ByVal ColName As String) As List(Of Integer)
            Dim returnList As New List(Of Integer)

            Dim rd As SqlDataReader = Nothing

            Try
                rd = ExecuteReader()
                While rd.Read
                    returnList.Add(CInt(rd(ColName)))
                End While
            Catch ext As AugTechnicalException
                Throw
            Catch ex As Exception
                Throw New AugTechnicalException("No se pudo ejecutar leer desde Reader (GetListOfIntegers) : ", ex)

            Finally
                If rd IsNot Nothing Then
                    If Not rd.IsClosed Then
                        rd.Close()
                    End If
                    rd.Dispose()
                End If
                Desconectar()
            End Try
            Return returnList
        End Function
        Protected Function GetListOfString(ByVal ColName As String) As List(Of String)
            Dim returnList As New List(Of String)

            Dim rd As SqlDataReader = Nothing

            Try
                rd = ExecuteReader()
                While rd.Read
                    returnList.Add(rd(ColName).ToString)
                End While
            Catch ext As AugTechnicalException
                Throw
            Catch ex As Exception
                Throw New AugTechnicalException("No se pudo ejecutar leer desde Reader (GetListOfIntegers) : ", ex)

            Finally
                If rd IsNot Nothing Then
                    If Not rd.IsClosed Then
                        rd.Close()
                    End If
                    rd.Dispose()
                End If
                Desconectar()
            End Try
            Return returnList
        End Function
        Protected Function LoadAll(ByVal oReader As SqlDataReader) As IList
            Dim oListRet As IList = GetList()
            While oReader.Read()
                AddObjectToList(oListRet, oReader)
            End While
            Return oListRet
        End Function

        Private Sub AddObjectToList(ByVal oLst As IList, ByVal oRd As SqlDataReader)
            Dim obj As Object = DoLoad(oRd)
            If obj IsNot Nothing Then oLst.Add(obj)
        End Sub

        Private Sub LoadAllChildEntities(ByVal EntityList As IList)
            If EntityList IsNot Nothing Then
                For Each Entidad As Entity In EntityList
                    If Entidad IsNot Nothing Then DoLoadChildEntities(Entidad)
                Next
            End If
        End Sub



#Region "Get Values"

        Private Function HasColumn(ByVal reader As SqlDataReader, ByVal colName As String) As Boolean
            For i As Integer = 0 To reader.FieldCount - 1
                If reader.GetName(i).Equals(colName, StringComparison.InvariantCultureIgnoreCase) Then Return True
            Next
            Return False
        End Function

        Protected Function GetColumnValue(ByVal reader As SqlDataReader, ByVal colNum As Integer, Optional ByVal nullValue As Object = Nothing) As Object
            If reader(colNum) Is DBNull.Value Then
                Return nullValue
            Else
                Return reader(colNum)
            End If
        End Function

        Protected Function GetObject(ByVal reader As SqlDataReader, ByVal colName As String, Optional ByVal nullValue As Object = Nothing) As Object

            If Not HasColumn(reader, colName) OrElse reader(colName) Is DBNull.Value Then Return nullValue

            Return reader(colName)

        End Function

        Protected Function GetString(ByVal reader As SqlDataReader, ByVal colName As String, Optional ByVal nullValue As String = Nothing) As String

            If Not HasColumn(reader, colName) OrElse reader(colName) Is DBNull.Value Then Return nullValue

            Return reader(colName).ToString()

        End Function

        Protected Function GetChar(ByVal reader As SqlDataReader, ByVal colName As String, Optional ByVal nullValue As Char = Nothing) As Char

            If Not HasColumn(reader, colName) OrElse reader(colName) Is DBNull.Value OrElse Len(reader(colName)) < 1 Then Return nullValue

            Return Convert.ToChar(reader(colName))

        End Function

        Protected Function GetCharNullable(ByVal reader As SqlDataReader, ByVal colName As String, Optional ByVal nullValue As Char = Nothing) As Nullable(Of Char)

            If Not HasColumn(reader, colName) OrElse reader(colName) Is DBNull.Value OrElse Len(reader(colName)) < 1 Then Return nullValue

            Return Convert.ToChar(reader(colName))

        End Function

        Protected Function GetInt(ByVal reader As SqlDataReader, ByVal colName As String, Optional ByVal nullValue As Integer = -1) As Integer

            If Not HasColumn(reader, colName) OrElse reader(colName) Is DBNull.Value Then Return nullValue

            Return CInt(reader(colName))

        End Function

        Protected Function GetIntNullable(ByVal reader As SqlDataReader, ByVal colName As String) As Nullable(Of Integer)

            If Not HasColumn(reader, colName) OrElse reader(colName) Is DBNull.Value Then Return Nothing

            Return Convert.ToInt32(reader(colName))

        End Function

        Protected Function GetDateTime(ByVal reader As SqlDataReader, ByVal colName As String) As DateTime

            If Not HasColumn(reader, colName) OrElse reader(colName) Is DBNull.Value Then Return Nothing

            Return Convert.ToDateTime(reader(colName))

        End Function

        Protected Function GetDateTime(ByVal reader As SqlDataReader, ByVal colName As String, ByVal nullValue As Nullable(Of DateTime)) As Nullable(Of DateTime)

            If Not HasColumn(reader, colName) OrElse reader(colName) Is DBNull.Value Then Return Nothing

            Return Convert.ToDateTime(reader(colName))

        End Function

        Protected Function GetDateTimeNullable(ByVal reader As SqlDataReader, ByVal colName As String, ByVal nullValue As Nullable(Of DateTime)) As Nullable(Of DateTime)

            If Not HasColumn(reader, colName) OrElse reader(colName) Is DBNull.Value Then Return Nothing

            Return Convert.ToDateTime(reader(colName))

        End Function


        Protected Function GetBoolean(ByVal reader As SqlDataReader, ByVal colName As String, Optional ByVal nullValue As Boolean = False) As Boolean

            If Not HasColumn(reader, colName) OrElse reader(colName) Is DBNull.Value Then Return nullValue

            Return GetInt(reader, colName) = 1

        End Function

        Protected Function GetNullableBoolean(ByVal reader As SqlDataReader, ByVal colName As String) As Nullable(Of Boolean)

            If Not HasColumn(reader, colName) OrElse reader(colName) Is DBNull.Value Then Return Nothing

            Return GetInt(reader, colName) = 1

        End Function

        Protected Function GetFloat(ByVal reader As SqlDataReader, ByVal colName As String, ByVal nullValue As Single) As Single

            If Not HasColumn(reader, colName) OrElse reader(colName) Is DBNull.Value Then Return nullValue

            Return CSng(reader(colName))

        End Function

        Protected Function GetDouble(ByVal reader As SqlDataReader, ByVal colName As String, ByVal nullValue As Double) As Double

            If Not HasColumn(reader, colName) OrElse reader(colName) Is DBNull.Value Then Return nullValue

            Return Convert.ToDouble(reader(colName))

        End Function
        Protected Function GetDecimal(ByVal reader As SqlDataReader, ByVal colName As String, Optional ByVal nullValue As Decimal = 0) As Decimal

            If Not HasColumn(reader, colName) OrElse reader(colName) Is DBNull.Value Then Return nullValue

            Return Convert.ToDecimal(reader(colName))

        End Function

        Protected Function GetNullableDecimal(ByVal reader As SqlDataReader, ByVal colName As String) As Decimal?

            If Not HasColumn(reader, colName) OrElse reader(colName) Is DBNull.Value Then Return Nothing

            Return Convert.ToDecimal(reader(colName))

        End Function

        Protected Function GetByteArray(ByVal reader As SqlDataReader, ByVal colName As String) As Byte()

            If Not HasColumn(reader, colName) OrElse reader(colName) Is DBNull.Value Then Return Nothing

            Return DirectCast(reader(colName), Byte())

        End Function

        Protected Sub GetUniqueID(ByVal reader As SqlDataReader, ByVal entidad As Entity)

            If Not HasColumn(reader, IDUNICO) OrElse reader(IDUNICO) Is DBNull.Value Then
                entidad.idUnico = Guid.Empty
                Return
            End If

            entidad.idUnico = DirectCast(reader(IDUNICO), Guid)

        End Sub

        Protected Function GetUniqueID(ByVal reader As SqlDataReader, ByVal colName As String) As Guid

            If Not HasColumn(reader, IDUNICO) OrElse reader(colName) Is DBNull.Value Then Return Guid.Empty

            Return DirectCast(reader(colName), Guid)

        End Function

        Protected Function GetNullableUniqueID(ByVal reader As SqlDataReader, ByVal colName As String) As Nullable(Of Guid)

            If Not HasColumn(reader, IDUNICO) OrElse reader(colName) Is DBNull.Value Then Return Nothing

            Return DirectCast(reader(colName), Guid)

        End Function


        Protected Function GetOutIntNullable(ByVal paramName As String) As Nullable(Of Integer)
            If _command.Parameters(paramName).Value Is DBNull.Value Then
                Return Nothing
            Else
                Return CInt(_command.Parameters(paramName).Value)
            End If
        End Function


#End Region

#Region "Add Parameters"

        Private Sub AddParameterValue(ByVal paramName As String, ByVal value As Object)

            If Not _command.Parameters.Contains(paramName) Then
                If TypeOf value Is Integer Then
                    _command.Parameters.Add(paramName, SqlDbType.Int)
                End If
                If TypeOf value Is String Then
                    _command.Parameters.Add(paramName, SqlDbType.VarChar, DirectCast(value, String).Length)
                End If
                If TypeOf value Is DateTime Then
                    _command.Parameters.Add(paramName, SqlDbType.DateTime)
                End If
                If TypeOf value Is Double Then
                    _command.Parameters.Add(paramName, SqlDbType.Float)
                End If
                If TypeOf value Is Byte() Then
                    _command.Parameters.Add(paramName, SqlDbType.Image)
                End If
                If TypeOf value Is Guid Then
                    _command.Parameters.Add(paramName, SqlDbType.UniqueIdentifier)
                End If
                If TypeOf value Is Decimal Then
                    _command.Parameters.Add(paramName, SqlDbType.Decimal)
                End If
                If Not _command.Parameters.Contains(paramName) Then
                    _command.Parameters.Add(paramName, SqlDbType.VarChar)
                End If
            End If
            If value IsNot Nothing Then
                _command.Parameters(paramName).Value = value
            Else
                _command.Parameters(paramName).Value = DBNull.Value
            End If

        End Sub



        Protected Sub AddParameterInt(ByVal paramName As String, ByVal value As Integer)
            AddParameterValue(paramName, value)
        End Sub

        Protected Sub AddParameter(ByVal paramName As String, ByVal value As Integer)
            If value >= 0 Then
                AddParameterValue(paramName, value)
            Else
                AddNullIntParameter(paramName)
            End If
        End Sub

        Protected Sub AddParameter(ByVal paramName As String, ByVal value As Nullable(Of Integer))
            If value.HasValue Then
                AddParameter(paramName, value.Value)
            Else
                AddNullIntParameter(paramName)
            End If
        End Sub

        Protected Sub AddParameter(ByVal paramName As String, ByVal value As Nullable(Of Char))
            If value.HasValue Then
                AddParameter(paramName, value.Value.ToString)
            Else
                AddNullStringParameter(paramName)
            End If
        End Sub


        Protected Sub AddParameter(ByVal paramName As String, ByVal value As Nullable(Of Boolean))
            If value.HasValue Then
                AddParameter(paramName, value.Value)
            Else
                AddNullStringParameter(paramName)
            End If
        End Sub

        Protected Sub AddParameter(ByVal paramName As String, ByVal value As Byte())
            AddParameterValue(paramName, value)
        End Sub

        Protected Sub AddParameter(ByVal paramName As String, ByVal value As Boolean)
            AddParameterValue(paramName, IIf(value, 1, 0))
        End Sub

        Protected Sub AddParameter(ByVal paramName As String, ByVal value As String)
            AddParameterValue(paramName, value)
        End Sub

        Protected Sub AddParameter(ByVal paramName As String, ByVal value As Nullable(Of DateTime))
            If value.HasValue Then
                AddParameter(paramName, value.Value)
            Else
                AddNullDateTimeParameter(paramName)
            End If
        End Sub

        Protected Sub AddParameter(ByVal paramName As String, ByVal value As DateTime)
            AddParameterValue(paramName, value)
        End Sub

        Protected Sub AddParameter(ByVal paramName As String, ByVal value As Double)
            AddParameterValue(paramName, value)
        End Sub
        Protected Sub AddParameter(ByVal paramName As String, ByVal value As Guid)
            AddParameterValue(paramName, value)
        End Sub
        Protected Sub AddParameter(ByVal paramName As String, ByVal value As Decimal)
            AddParameterValue(paramName, value)
        End Sub

        Protected Sub AddParameter(ByVal paramName As String, ByVal value As Decimal?)
            AddParameterValue(paramName, value)
        End Sub

        Protected Sub AddParameter(ByVal paramName As String, ByVal value As Guid?)
            AddParameterValue(paramName, value)
        End Sub


        Protected Sub AddUniqueIdParameter(ByVal entidad As Entity)
            'If entidad.idUnico = Guid.Empty Then entidad.idUnico = Guid.NewGuid ' Cargo idUnico si la entidad no lo tiene
            If (entidad.idUnico = Guid.Empty) Then
                _command.Parameters.Add("@" & IDUNICO, SqlDbType.UniqueIdentifier).Value = DBNull.Value
            Else
                _command.Parameters.Add("@" & IDUNICO, SqlDbType.UniqueIdentifier).Value = entidad.idUnico
            End If

        End Sub

        Protected Sub AddNullDateTimeParameter(ByVal paramName As String)
            If Not _command.Parameters.Contains(paramName) Then _command.Parameters.Add(paramName, SqlDbType.DateTime)
            _command.Parameters(paramName).Value = DBNull.Value
        End Sub
        Protected Sub AddNullIntParameter(ByVal paramName As String)
            If Not _command.Parameters.Contains(paramName) Then _command.Parameters.Add(paramName, SqlDbType.Int)
            _command.Parameters(paramName).Value = DBNull.Value
        End Sub
        Protected Sub AddNullStringParameter(ByVal paramName As String)
            If Not _command.Parameters.Contains(paramName) Then _command.Parameters.Add(paramName, SqlDbType.VarChar)
            _command.Parameters(paramName).Value = DBNull.Value
        End Sub
        Protected Sub AddParameterOut(ByVal paramName As String)
            If Not _command.Parameters.Contains(paramName) Then
                Dim paramResultado As SqlParameter = New SqlParameter(paramName, SqlDbType.Int)
                paramResultado.Direction = ParameterDirection.Output
                _command.Parameters.Add(paramResultado)
            End If
        End Sub
#End Region

    End Class
End Namespace
