

function Bind_Entity_Drpdwn(data) {
    $("#drpEntity").html("");
    var htmltext = "";
    htmltext += "<option value=''>-Select Entity-</option>";
    if (data.length > 0) {
        for (i = 0; i < data.length; i++) {
            htmltext += "<option value='" + data[i].Entity_Id + "'>" + data[i].Entity_Name + "</option>";
        }
    }
    $("#drpEntity").html(htmltext);
}