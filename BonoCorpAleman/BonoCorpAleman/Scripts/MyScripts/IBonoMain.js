$(document).ready(function () {
    /*$('.enPorcentaje').keyup(function () {
        var newval = $(this).val().substring($(this).length - 1, $(this).length);
        $(this).val(newval + "%");
    });
    $('.enPorcentaje').submit(function () {
        var newNum = $(this).substring(0, $(this).length - 1);
        newNum = parseFloat(newNum);
        return newNum;
    });*/
    function getoptionCapitalizacion(lst) {
        var str = "";
        lst.forEach(function (element, index) {
            //Interpolacion de string //alt + 96 para comillas de interpolacion
            str += `<option value="${element.ID}">${element.Nombre}</option>`;
        });
        return str;
    }
    function getoptionTipoPlazoGracia(lst) {
        var str = "";
        lst.forEach(function (element, index) {
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

    var Bono_selectTipoTasa = "<select class='form-control valid' data-val='true' name='LstTasaId'><option value=''>Tipo de Tasa</option><option value='1'>Nominal</option><option value='2'>Efectiva</option></select>";
    var Bono_inputPorcentaje = "<input class='form-control text-box single-line valid' name='LstValorTasa' type='number' placeholder='valor de tasa(%)'>";
    var LstCapitalizacion = [
                                { "ID": "1", "Nombre": "Diaria" },
                                { "ID": "2", "Nombre": "Quincenal" },
                                { "ID": "3", "Nombre": "Mensual" },
                                { "ID": "4", "Nombre": "Bimestral" },
                                { "ID": "5", "Nombre": "Trimestral" },
                                { "ID": "6", "Nombre": "Cuatrimestral" },
                                { "ID": "7", "Nombre": "Semestral" },
                                { "ID": "8", "Nombre": "Anual" }
    ];

    var Bono_selectCapitalizacion = "<select class='form-control valid' data-val='true' name='LstCapitalizaciones'><option value=''>Capitalizacion?</option>" + getoptionCapitalizacion(LstCapitalizacion) + "</select>";
    var Bono_inputPeriodo = "<input class='form-control text-box single-line valid' name='LstNroPeriodos' type='number' placeholder='En que periodo'>";
    $("#btnAddTasa").click(function () {
        //alert("click");
        $("#tableTRtasa1").append(`<td> ${Bono_selectTipoTasa} </td>`);
        $("#tableTRtasa2").append(`<td> ${Bono_inputPorcentaje} </td>`);
        $("#tableTRtasa3").append(`<td> ${Bono_selectCapitalizacion} </td>`);
        $("#tableTRtasa4").append(`<td> ${Bono_inputPeriodo} </td>`);
    });
    var PG_inputPeriodo = `<input class="form-control text-box single-line valid" name="LstPerPlazoBono" type="number" placeholder="En que periodo">`;
    var LstPlazoGracia = [
                                { "ID": "1", "Nombre": "Sin Plazo" },
                                { "ID": "2", "Nombre": "Parcial" },
                                { "ID": "3", "Nombre": "Total" }
    ];
    var PG_selectTipo = '<select class="form-control valid" data-val="true" name="LstPlazosDeGraciaId"><option value="">[-Seleccionar-]</option>'+getoptionTipoPlazoGracia(LstPlazoGracia)+'</select>';
    $("#btnAddPlazoG").click(function () {
        console.log(getoptionTipoPlazoGracia(LstPlazoGracia));
        $("#tablePGTR1").append(`<td> ${PG_selectTipo} </td>`);
        $("#tablePGTR2").append(`<td> ${PG_inputPeriodo} </td>`);
    });
    var Inf_inputPorcentaje = `<input class="form-control text-box single-line valid" name="LstValInflaciones" type="number" placeholder="Porcentaje">`;
    var Inf_inputPeriodo = `<input class="form-control text-box single-line valid" name="LstPerInflaciones" type="number" placeholder="En que periodo">`;
    $("#btnAddInflacion").click(function () {
        //alert("click");
        $("#tableInflacionTR1").append(`<td> ${Inf_inputPorcentaje} </td>`);
        $("#tableInflacionTR2").append(`<td> ${Inf_inputPeriodo} </td>`);
    });
    //IMPORTANTE
    //si el los archivos .js no se actualizan, cuando ya este el proyecto cargado en el navegador
    //presionar CTRL + F5
});
