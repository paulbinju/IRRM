    $(document).ready(function () {


        $("#btnAttach").click(function () {

            if ($("#DocumentTypeID").val() == '' || $("#attachdescription").val() == '' || $("#attachfile").val() == '') {
                alert('Document Type / Description / File name are mandatory!');
            }
            else {
                var filename = $('input[type=file]').val().replace(/C:\\fakepath\\/i, '')

                $.ajax({
                    method: "POST",
                    url: "@Url.Content("~/AddAttachment")",
                    data: {
                        DocumentTypeID: $('#DocumentTypeID').val(), Description: $('#attachdescription').val(), FileName: filename
                    },
                    success: function (data) {
                        $('#DocumentTypeID').val('');
                        $('#attachdescription').val('');
                        $('#attachfile').val('');
                        $.each(data, function (key, value) {
                            loadAttachment(data[key].IncidentID);
                        });
                    },
                    error: function () {
                        alert("Couldn't Save, check the input values!");
                    }
                });
            }
        });



        $("#btnWitness").click(function () {

            if ($("#witnessname").val() == '' || $("#witnessbranchid").val() == '' || $("#witnessphone").val() == '') {
                alert('Name / Branch / Phone are mandatory!');
            }
            else {



                $.ajax({
                    method: "POST",
                    url: "@Url.Content("~/AddWitness")",
                    data: {
                        Name: $('#witnessname').val(), Email: $('#witnessemail').val(), Phone: $('#witnessphone').val(), BranchID: $('#witnessbranchid').val(), Designation: $('#witnessdesignation').val()
                    },
                    success: function (data) {
                        $('#witnessname').val('');
                        $('#witnessemail').val('');
                        $('#witnessphone').val('');
                        $('#witnessbranchid').val('');
                        $('#witnessdesignation').val('');
                        $.each(data, function (key, value) {
                            loadwitness(data[key].IncidentID);
                        });
                        
                    },
                    error: function () {
                        alert("Couldn't Save, check the input values!");
                    }
                });
            }
        });
    });


    function delattachment(IncidentAttachmentID, IncidentID) {
        $.ajax({
            method: "GET",
            url: "@Url.Content("~/DeleteAttachment/")" + IncidentAttachmentID + "/" + IncidentID,
            contentType: false,
            cache: false,
            processData: false,
            success: function (data) {

                loadAttachment(IncidentID);

            },
            error: function () {
            }
        });

    }

    
    function loadAttachment(IncidentID) {
        $.ajax({
            method: "GET",
            url: "@Url.Content("~/ViewAttachment/")" + IncidentID,
            contentType: false,
            cache: false,
            processData: false,
            success: function (data) {
                $("#tblAttachments").children().remove();

                var str = "<table  class='table table-striped col-md-12' >";
                str += "<tr><th>Document Type</th><th>Description</th><th>File Name</th></tr>";
                $.each(data, function (key, value) {
                    str += "<tr>" + "<td>" + data[key].DocumentType + "</td><td>" + data[key].Description + "</td><td>" + data[key].FileName + "</td><td><i class='fa fa-trash' style='color:red;cursor:pointer' onclick=delattachment(" + data[key].IncidentDocumentID + "," + data[key].IncidentID + ")></i></td></tr>";
                });
                str += "</table>"

                $("#tblAttachments").append(str);

            },
            error: function () {
            }
        });
    }








    function delwitness(IncidentWitnessID, IncidentID) {
        $.ajax({
            method: "GET",
            url: "@Url.Content("~/DeleteWitness/")" + IncidentWitnessID + "/" + IncidentID,
            contentType: false,
            cache: false,
            processData: false,
            success: function (data) {

                loadwitness(IncidentID);

            },
            error: function () {
            }
        });

    }

 
    function loadwitness(IncidentID) {
        $.ajax({
            method: "GET",
            url: "@Url.Content("~/ViewWitness/")" + IncidentID,
            contentType: false,
            cache: false,
            processData: false,
            success: function (data) {
                $("#tblwitness").children().remove();

                var str = "<table  class='table table-striped col-md-12' >";
                str += "<tr><th>Name</th><th>Email</th><th>Phone</th><th>Designation</th><th>Branch</th><th>Action</th></tr>";
                $.each(data, function (key, value) {
                    str += "<tr>" + "<td>" + data[key].Name + "</td><td>" + data[key].Email + "</td><td>" + data[key].Phone + "</td><td>" + data[key].Designation + "</td><td>" + data[key].Branch + "</td><td><i class='fa fa-trash' style='color:red;cursor:pointer' onclick=delwitness(" + data[key].IncidentWitnessID + "," + data[key].IncidentID + ")></i></td></tr>";

                });
                str += "</table>"

                $("#tblwitness").append(str);

            },
            error: function () {
            }
        });
    }

