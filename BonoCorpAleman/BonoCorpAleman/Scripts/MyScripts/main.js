$(document).ready(function () {
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

    $("#btnAddTasa").click(function () {
        //alert("click");
        $("#tableTRtasa1").append("<td>" + dwn + "</td>");
        $("#tableTRtasa2").append("<td>" + inpt + "</td>");
    });
});
