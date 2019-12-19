
$(document).ready(function () {
    var classroom = new Classroom();
});

class Classroom {
    constructor() {
        this.InitEvents();
        this.LoadData(this.GetData());
    }

    InitEvents() {
        $('#ClassroomTable tbody').on('click', 'tr', this.ClassroomTick);
        $('#ClassroomRow').on('click', '#add', this.OpenAddClassroom.bind(this));
        $('#ClassroomRow').on('click', '#delete', this.DeleteClassroom.bind(this));
        $('#ClassroomModal').on('click', '#save', this.AddClassroom.bind(this));
    }

    DeleteClassroom() {
        let me = this;
        let Cselected = $('#ClassroomTable .table-active')[0];
        let ClassroomID = Cselected.getAttribute('recordid');
        $.ajax({
            method: 'delete',
            url: '/api/Classrooms/' + ClassroomID,
            async: false
        });
        this.LoadData(this.GetData());
    }

    AddClassroom() {
        let me = this;
        let object = {};
        object['phongThiName'] = $('#ClassroomModal [name=phongThiName]').val();
        object['computerNumber'] = parseInt($('#ClassroomModal [name=computerNumber]').val());
        debugger
        $.ajax({
            method: 'Post',
            url: '/api/Classrooms/',
            data: JSON.stringify(object),
            async: false,
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        });
        $('#ClassroomModal').modal('hide');
        this.LoadData(this.GetData());
    }

    OpenAddClassroom() {
        $('#ClassroomModal').modal('show');
    }

    ClassroomTick() {
        $('#ClassroomTable tr').removeClass('table-active');
        let row = this;
        $(row).addClass('table-active');
    }

    GetData() {
        let data = [];
        $.ajax({
            method: 'GET',
            url: '/api/Classrooms',
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
        
        var fields = $('#ClassroomTable th[name]');
        $('#ClassroomTable tbody').empty();
        $.each(data, function (index, item) {
            var rowHTML = $('<tr recordID = "' + item['phongThiId'] + '"></tr>');

            $.each(fields, function (fieldIndex, fieldItem) {
                var name = fieldItem.getAttribute('name');
                var value = item[name];
                if (name != 'action') {
                    rowHTML.append('<td name="' + name + '"><span class="text">' + value + '</span></td>');
                }
                else {
                    rowHTML.append('<button class="btn btn-outline-primary btn-sm" id="show">Xem ca thi</button>');
                }

            });
            $('#ClassroomTable tbody').append(rowHTML);
        });
    }
}