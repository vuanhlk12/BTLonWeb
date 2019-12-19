
$(document).ready(function () {
    var student = new Student();
});

class Student {
    constructor() {
        this.InitEvents();
        this.LoadData(this.GetData());
    }

    InitEvents() {
        $('#StudentTable tbody').on('click', 'tr', this.UserTick);
        $('#StudentRow').on('click', '#add', this.OpenAddUser.bind(this));
        $('#StudentRow').on('click', '#delete', this.DeleteUser.bind(this));
        $('#StudentRow').on('click', '#show', this.ShowMonThiByUser.bind(this));
        $('#StudentModal').on('click', '#save', this.AddUser.bind(this));
        $("#StudentLoader").change(this.LoadStudentFromExcel.bind(this));
        $("#SubjectLoader").change(this.LoadSubjectForStudentFromExcel.bind(this));
    }

    LoadSubjectForStudentFromExcel(evt) {
        let me = this;
        let selectedFile = evt.target.files[0];
        let reader = new FileReader();
        reader.onload = function (event) {
            let data = event.target.result;
            let workbook = XLSX.read(data, { type: "binary" });
            workbook.SheetNames.forEach(function (sheetName) {
                let XL_row_object = XLSX.utils.sheet_to_row_object_array(workbook.Sheets[sheetName]);
                debugger
                $.each(XL_row_object, function (index, item) {
                })
                $.ajax({
                    method: 'Post',
                    url: '/api/Sv/PostSubjectForStudent',
                    data: JSON.stringify(XL_row_object),
                    async: false,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function () {

                        debugger
                    }
                });
                me.LoadData(me.GetData());
                $("#SubjectLoader").val(null);

            });
        }
        reader.readAsBinaryString(selectedFile);
    }

    LoadStudentFromExcel(evt) {
        let me = this;
        let selectedFile = evt.target.files[0];
        let reader = new FileReader();
        reader.onload = function (event) {
            let data = event.target.result;
            let workbook = XLSX.read(data, { type: "binary" });
            workbook.SheetNames.forEach(function (sheetName) {
                let XL_row_object = XLSX.utils.sheet_to_row_object_array(workbook.Sheets[sheetName]);
                $.each(XL_row_object, function (index, item) {
                    item['birth'] = new Date(item['birth']).addHours(7);
                    item['userIdfake'] = item['userIdfake'].toString();
                    item['currentKiThi'] = $('#CurrentKyThi option:selected').val();
                })
                $.ajax({
                    method: 'Post',
                    url: '/api/Sv/PostMulti',
                    data: JSON.stringify(XL_row_object),
                    async: false,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function () {
                        
                        debugger
                    }
                });
                me.LoadData(me.GetData());
                $("#StudentLoader").val(null);
                
            });
        }
        reader.readAsBinaryString(selectedFile);
    }

    LoadDataMonThiByUser(data) {
        var fields = $('#SubjectTable th[name]');
        $('#SubjectTable tbody').empty();
        $.each(data, function (index, item) {
            var rowHTML = $('<tr recordID = "' + item['kyThiId'] + '"></tr>');
            $.each(fields, function (fieldIndex, fieldItem) {
                var name = fieldItem.getAttribute('name');
                var value = item[name];
                if (name == 'isValid') {
                    if (item[name] == false) {
                        value = 'Không';
                        rowHTML.append('<td name="' + name + '"><span class="badge badge-danger">' + value + '</span></td>');
                    } else {
                        value = 'Có';
                        rowHTML.append('<td name="' + name + '"><span class="badge badge-success">' + value + '</span></td>');
                        
                    }
                } else {
                    rowHTML.append('<td name="' + name + '"><span class="text">' + value + '</span></td>');
                }
            });
            $('#SubjectTable tbody').append(rowHTML);
        });
    }

    GetDataMonThiByUser(KyThiID, UserID) {
        
        let data = [];
        $.ajax({
            method: 'GET',
            url: `/api/Subjects/GetMonThiByKyThi/${KyThiID}/${UserID}`,
            async: false,
            success: function (res) {
                data = res;
            },
            error: function (res) {
            }
        });

        return data;
    }

    ShowMonThiByUser() {
        let me = this;
        $('#SubjectModal').modal('show');
        let KyThiID = $('#CurrentKyThi option:selected').val();
        let UserID = $('#StudentTable .table-active')[0].getAttribute('recordid');
        me.LoadDataMonThiByUser(me.GetDataMonThiByUser(KyThiID, UserID));

    }

    DeleteUser() {
        let me = this;
        let Uselected = $('#StudentTable .table-active')[0];
        let UserID = Uselected.getAttribute('recordid');
        $.ajax({
            method: 'delete',
            url: '/api/Sv/' + UserID,
            async: false
        });
        this.LoadData(this.GetData());
    }

    AddUser() {
        let me = this;
        let object = {};
        object['userIdfake'] = $('#StudentModal [name=userIdfake]').val();
        object['userName'] = $('#StudentModal [name=userName]').val();
        object['password'] = $('#StudentModal [name=password]').val();
        object['name'] = $('#StudentModal [name=name]').val();
        object['birth'] = new Date($('#StudentModal [name=birth]').val());
        $.ajax({
            method: 'Post',
            url: '/api/Sv/',
            data: JSON.stringify(object),
            async: false,
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        });
        $('#StudentModal').modal('hide');
        this.LoadData(this.GetData());
    }

    OpenAddUser() {
        $('#StudentModal').modal('show');
    }

    UserTick() {
        $('#StudentTable tr').removeClass('table-active');
        let row = this;
        $(row).addClass('table-active');
    }

    GetData() {
        let data = [];
        $.ajax({
            method: 'GET',
            url: '/api/Sv',
            async: false,
            success: function (res) {
                data = res;
            },
            error: function (res) {
            }
        });

        return data;


    }

    LoadData(data) {

        var fields = $('#StudentTable th[name]');
        $('#StudentTable tbody').empty();
        $.each(data, function (index, item) {
            var rowHTML = $('<tr recordID = "' + item['userId'] + '"></tr>');

            $.each(fields, function (fieldIndex, fieldItem) {
                var name = fieldItem.getAttribute('name');
                var value = item[name];
                if (name == 'birth') {
                    let d = new Date(value);
                    value = d.formatddMMyyyy();
                }
                rowHTML.append('<td name="' + name + '"><span class="text">' + value + '</span></td>');
            });
            $('#StudentTable tbody').append(rowHTML);
        });
    }
}