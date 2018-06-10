$(document).ready(function () {
    function getoptionCapitalizacion(lst) {
        var str = "";
        lst.forEach(function (element, index) {
            //Interpolacion de string //alt + 96 para comillas de iterpolacion
            str += `<option value="${element.ID}">${element.Nombre}</option>`;
        });
        return str;
    }
    $("#CapBono").hide();
    $("#TasaId").change(function () {
        if ($(this).val() === "1") {
            $("#CapBono").show();
        }
        else {
            $("#CapBono").hide();
        }
    });

    var dwn = "<select class='form-control valid' data-val='true' name='LstTasaId'><option value=''>[-Seleccionar-]</option><option value='1'>Nominal</option><option value='2'>Efectiva</option></select>";
    var inpt = "<input class='form-control text-box single-line valid' name='LstValorTasa' type='number' value='0'>";
    var LstCapitalizacion = [
                                { "ID": "1", "Nombre": "Diaria" },
                                { "ID": "2", "Nombre": "Quincenal" },
                                { "ID": "3", "Nombre": "Mensual" },
                                { "ID": "4", "Nombre": "Bimestral" },
                                { "ID": "5", "Nombre": "Trimestral" },
                                { "ID": "6", "Nombre": "Cuatrimestral" },
                                { "ID": "7", "Nombre": "Semestral" },
                                { "ID": "8", "Nombre": "Anual" },
    ];

    var dwn2 = "<select class='form-control valid' data-val='true' name='LstCapitalizaciones'><option value=''>[-Seleccionar-]</option>" + getoptionCapitalizacion(LstCapitalizacion) + "</select>";
    var inp2 = "<input class='form-control text-box single-line valid' name='LstNroPeriodos' type='number' value='0'>";
    $("#btnAddTasa").click(function () {
        //alert("click");
        $("#tableTRtasa1").append(`<td> ${dwn} </td>`);
        $("#tableTRtasa2").append(`<td> ${inpt} </td>`);
        $("#tableTRtasa3").append(`<td> ${dwn2} </td>`);
        $("#tableTRtasa4").append(`<td> ${inp2} </td>`);
    });
});
