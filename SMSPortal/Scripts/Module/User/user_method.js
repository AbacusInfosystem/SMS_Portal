function Bind_Entity(data) {
    $("#drpEntity").html("");

    var htmltext = "";

    htmltext += "<option ='0'>-Select Entity-</option>";

    if (data.length > 0) {
        for (var i = 0; i < data.length ; i++) {
            htmltext += "<option value='" + data[i].Entity_Id + "'>" + data[i].Entity_Name + "</option>";
        }
    }
    $("#drpEntity").html(htmltext);

}