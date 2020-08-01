$(document).ready(function () {
    $('input[type=text],input[type=radio],input[type=checkbox],textarea,select,input[type=email],input[type=number]').each(function () {
        var req = $(this).attr('data-val-required');
        var exclude = $(this).attr('data-exclude');
        if (undefined != req && undefined == exclude) {
            var label = $('label[for="' + $(this).attr('id') + '"]');
            var text = label.text();
            if (text.length > 0) {
                label.append('<span style="color:red"> *</span>');
            }
        }
    });
    LoadList();
});

function AllowNumbers(evt) {
    var keyCode = (evt.which) ? evt.which : event.keyCode;
    if (keyCode < 48 || keyCode >= 58 && keyCode != 8) {
        return false;
    }
    return true;
}

$('#btnSave').click(function () {

    if (window.FormData !== undefined) {


        var fileUpload = $("#imgUpload").get(0);
        var files = fileUpload.files;

        if ($('#ProductName').val() == '' || $('#ProductPrice').val() == '' || $('#ProductDesc').val() == '') {
            alertify.error('Please fill all the mandatory fields.');
        }
       
        else {
            var fileData = new FormData();

            fileData.append("ProductName", $('#ProductName').val());
            fileData.append("ProductPrice", $('#ProductPrice').val());
            fileData.append("ProductDesc", $('#ProductDesc').val());
            if (files.length > 0) {
                
                fileData.append(files[0].name, files[0]);
                fileData.append("ProductImage", files[0].name);
            }
            

            $.ajax({
                url: '/Products/SaveProduct',
                type: "POST",
                contentType: false, // Not to set any content header  
                processData: false, // Not to process data  
                data: fileData,
                success: function (result) {
                    if (result == 'Saved Successfully') {
                        alertify.success(result);
                        formReset();
                    }
                    else {
                        alertify.error(result);
                    }
                    
                    LoadList();
                },
                error: function (err) {
                    alertify.error("Something went wrong. Please try again.");
                }
            });
        }        
    } else {
        alertify.error("FormData is not supported.");
    }
});

function OnSuccess(response) {
    $("#tblProducts").DataTable().destroy();
    $("#tblProducts").DataTable(
        {
            bLengthChange: true,
            lengthMenu: [[5, 10, -1], [5, 10, "All"]],
            bFilter: true,
            bSort: true,
            bPaginate: true,
            data: response,
            columns: [
            {
                "render": function (data, type, full) {
                    return '<a href="/Products/ProductDetail?ProductId=replace">ProductName</a>'.replace("replace", full.ProductId).replace("ProductName", full.ProductName);
                }
            },
            { 'data': 'ProductPrice' },
            {
                "render": function (data, type, JsonResultRow, meta) {
                    if (JsonResultRow.ProductImage == null)
                        return '<img height="50px" width="50px" src="/Content/Images/no-image-icon-5.jpg" />';
                    else
                        return '<img height="50px" width="50px" src="/ProductImages/' + JsonResultRow.ProductImage + '" />';
                }
             },
             {
                    "render": function (data, type, full) {
                     return '<button type="button" class="btn btn-sm btn-danger" onclick="ConfirmDelete(ProductId)"><i class="glyphicon glyphicon-trash"></i></button>'.replace("ProductId", full.ProductId);
                }
             }
            ]
        });
};

function LoadList() {
    $.ajax({
        type: "GET",
        url: "/Products/GetProducts",
        data: '{}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnSuccess,
        failure: function (response) {
            alertify.error("Something went wrong. Please try again.");
        },
        error: function (response) {
            alertify.error("Something went wrong. Please try again.");
        }
    });
};


function ConfirmDelete(ProductId) {
    if (!confirm("Are you sure to delete?")) {
        return;
    }

    $.ajax({

        type: "POST",
        url: "/Products/DeleteProduct",
        data: { ProductId: ProductId },
        dataType: "json",
        success: function (response) {
            if (response) {
                alertify.success('Deleted Successfully');
                LoadList();
            }
            else {
                alertify.error("Something went wrong. Please try again.");
            }
        },
        error: function () {
            alertify.error("Something went wrong. Please try again.");
        }
    });
}

function formReset() {
    $("input[type=text], textarea").val("");
    $("#imgUpload").val(null);
}