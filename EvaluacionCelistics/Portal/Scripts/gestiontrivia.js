$(function () {

    $("#grid-data").bootgrid({
        caseSensitive: false,
        formatters: {
            "link": function (column, row) {
                return "<a href=\"/trivia.aspx?idUsuario=" + row.id + "\">Ver</a>";
            }
        }
    });

});