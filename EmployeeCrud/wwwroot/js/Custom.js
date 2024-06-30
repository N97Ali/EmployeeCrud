

$(document).ready(function () {
    EmployeeData();

});

//$('#Modelclass').click(function () {
//    $('#exampleModal').modal('show');
//    $('#HideId').hide();
//    $('#id').val(''),
//    $('#name').val('');
//    $('#address').val('');
//    $('#date').val('');
//    $('#status').val('');
//    $('#btnsave').css('display', 'block');
//    $('#btnedit').css('display', 'none');
//    $('#btndelete').css('display', 'none');
//    $('#exampleModalLabel').text('Add');
//});
function EmployeeData() {
    $.ajax({
        type: 'get',
        url: '/Ajax/GetAll',
        dataType: 'json',
        contentType: 'application/json;charset=utf-8;',
        success: function (data) {
            //console.log(JSON.stringify(data));
            if (data == null || data == undefined || data.length == 0) {
                var row = '';
                row += '<tr>';
                row += '<td "colspan = 5"> Undefined data Fatched : </td>';
                row += '</tr>';
                $('table_body').html(row);
            }
            else {
                var row = "";
                $.each(data, function (index, item) {
                    row += '<tr>';
                    row += '<td>' + item.id + '</td>';
                    row += '<td>' + item.name + '</td>';
                    row += '<td>' + item.address + '</td>';
                    row += '<td>' + item.date + '</td>';
                    row += '<td>' + item.status + '</td>';
                    row += '<td ><a href="#" class="btn btn-primary"  onclick="GetbyId(' + item.id + ')" >Edit</a> <a href="#" class="btn btn-danger" onclick="Delete(' + item.id + ')">Delete</a></td>';
                    row += '</tr>'
                });
                $('#table_body').html(row);
            }
        },
        error: function () {
            alert("date not recived form body");
        }
    });
}

function Add()
{
    var Newdata = {
        Name: $('#name').val(),
        Address: $('#address').val(),
        Date: $('#date').val(),
        Status: $('#status').val()

    }
    console.log(Newdata)
    $.ajax({
        url: '/Ajax/Add',
        type: 'post',
        data: JSON.stringify(Newdata), 
        datatype: 'json',
        contentType: 'application / json',
        
        success: function (response) {
            $('#name').val('');
            $('#address').val('');
            $('#date').val('');
            $('#status').val('');
            $('#exampleModal').modal('hide');
            EmployeeData();

        },
        error: function () {
            alert("data not found ");
        }


    });
}

function GetbyId(id) {
    $.ajax({
        type: "Get",
        url: "/Ajax/GetbyId/" + id,
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            $('#id').val(result.id);
            $('#name').val(result.name);
            $('#address').val(result.address);
            $('#date').val(result.date);
            $('#status').val(result.status);

            $("#exampleModal").modal('show');
            $("#HideId").show();
            $('#btnsave').css('display', 'none');
            $('#btnedit').css('display', 'block');
            $('#btndeleteById').css('display', 'block');
            $('#exampleModalLabel').text('Edit'); 
             
        },
        error: function (errormessage) {
            alert("Data not found ");
        }

    });
}

function Edit() {  
    var Newdata = {
        ID: $('#id').val(),
        Name: $('#name').val(),
        Address: $('#address').val(),
        Date: $('#date').val(),
        Status: $('#status').val()

    }
    $.ajax({
        type: 'post',
        url: '/Ajax/Edit',
        contentType: "application/json",
        data: JSON.stringify(Newdata),
        success: function (result) {
            $('#id').val(''),
            $('#name').val('');
            $('#address').val('');
            $('#date').val('');
            $('#status').val('');
            $('#exampleModal').modal('hide');
            $('#btnsave').css('display', 'block');
            $('#btnedit').css('display', 'none');
            $('#btndelete').css('display', 'none');
            $('#exampleModalLabel').text('Add'); 
            EmployeeData();
             
        },
        error: function (errormessage) {
            alert("no data");
        }
    });
}

//Not working right now ---
//function DeleteById(id) {
//    $.ajax({
//        type: "post",
//        url: "/Ajax/Delete/" + id,
//        contentType: "application/json",
//        data: {},
//        success: function (response) {
//            EmployeeData();
//        },
//        error: function (errormessage) {
//            alert("Not record found");
//        }
//    });
//}
function Delete(id){
    
        $.ajax({
            type: "post",
            url: "/Ajax/Delete/" + id,
            contentType: "application/json",
            data: {},
            success: function (response) {
                EmployeeData();
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
 
}

function Register() {
    var registerData = {
        Name: $('#name').val(),
        Email: $('#email').val(),
        Phone: $('#phone').val(),
        Password: $('#password').val()
    };
    console.log(registerData);
    $.ajax({
        type : 'post',
        url: '/Ajax/Register',
        data: JSON.stringify(registerData),
        dataType: 'json',
        contentType: 'application/json; charset=utf-8', 
        success: function (response) {
            $('#name').val('');
            $('#email').val('');
            $('#phone').val('');
            $('#password').val('');
            $('#exampleModal').modal('hide');
        },
        error: function () {
            alert("date not recived form body");
        }
    });
}

function Sign() {
    var loginData = {
        Email: $('#email').val(),
        Password: $('#password').val()
    };
    console.log(loginData)
    $.ajax({
        url: '/Ajax/SignIn',
        type: 'post',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(loginData),
        success: function () {
            $('#email').val('');
            $('#password').val('');
            $('#exampleModal1').modal('hide');

        },
        error: function () {

        }
    });
}
//function MyAccountData() {
//    $.ajax({
//        type: 'get',
//        url: '/Ajax/MyAccountIndex',
//        dataType: 'json',
//        contentType: 'application/json;charset=utf-8;',
//        success: function (data) {
//            console.log(JSON.stringify(data));
//            if (data == null || data == undefined || data.length == 0) {
//                var row = '';
//                row += '<tr>';
//                row += '<td "colspan = 5"> Undefined data Fatched : </td>';
//                row += '</tr>';
//                $('#tablebody').html(row);
//            }
//            else {
//                var row = "";
//                $.each(data, function (index, item) {
//                    row += '<tr>';
//                    row += '<td>' + item.Id + '</td>';
//                    row += '<td>' + item.Name + '</td>';
//                    row += '<td>' + item.Email + '</td>';
//                    row += '<td>' + item.Phone + '</td>';
//                    row += '<td>' + item.Password + '</td>';
//                    row += '<td ><a href="#" class="btn btn-primary"  onclick="GetbyId(' + item.Id + ')" >Edit</a> <a href="#" class="btn btn-danger" onclick="Delete(' + item.Id + ')">Delete</a></td>';
//                    row += '</tr>'
//                });
//                $('#tablebody').html(row);
//            }
//        },
//        error: function () {
//            alert("date not recived form body");
//        }
//    });
//}
//function Login() {
//    var loginData = {
//        Email: $('#email').val(),
//        Password: $('#password').val()
//    }
//    console.log(loginData);
//    $.ajax({
//        url: '/Ajax/Login',
//        type: 'post',
//        data: JSON.stringify(loginData),
//        datatype: 'json',
//        contentType: 'application/json',
//        success: function (response) {

//        },
//        error: function () {
//            alert("not login ");
//        }
//    });
//}



