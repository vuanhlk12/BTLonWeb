
$(document).ready(function () {
    let subject = new Subject();
});

class Subject {
    constructor() {
        this.InitEvents();
        this.LoadData(this.GetData());
    }

    InitEvents() {
        $('#SubjectTable tbody').on('click', 'tr', this.SubjectTick);
        $('#SubjectRow').on('click', '#add', this.OpenAddSubject.bind(this));
        $('#SubjectRow').on('click', '#delete', this.DeleteSubject.bind(this));
        $('#SubjectModal').on('click', '#save', this.AddSubject.bind(this));
    }

    DeleteSubject() {
        let me = this;
        let Sselected = $('#SubjectTable .table-active')[0];
        let SubjectID = Sselected.getAttribute('recordid');
        $.ajax({
            method: 'delete',
            url: '/api/Subjects/' + SubjectID,
            async: false
        });
        this.LoadData(this.GetData());
    }

    AddSubject() {
        let me = this;
        let object = {};
        object['monThiIdFake'] = $('#SubjectModal [name=monThiIdFake]').val();
        object['monThiName'] = $('#SubjectModal [name=monThiName]').val();
        object['giaoVien'] = $('#SubjectModal [name=giaoVien]').val();
        $.ajax({
            method: 'Post',
            url: '/api/Subjects/',
            data: JSON.stringify(object),
            async: false,
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        });
        $('#SubjectModal').modal('hide');
        this.LoadData(this.GetData());
    }

    OpenAddSubject() {
        $('#SubjectModal').modal('show');
    }

    SubjectTick() {
        $('#SubjectTable tr').removeClass('table-active');
        let row = this;
        $(row).addClass('table-active');
    }

    GetData() {
        let data = [];
        $.ajax({
            method: 'GET',
            url: '/api/Subjects',
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

        let fields = $('#SubjectTable th[name]');
        $('#SubjectTable tbody').empty();
        $.each(data, function (index, item) {
            let rowHTML = $('<tr recordID = "' + item['monThiId'] + '"></tr>');
            $.each(fields, function (fieldIndex, fieldItem) {
                let name = fieldItem.getAttribute('name');
                let value = item[name];
                rowHTML.append('<td name="' + name + '"><span class="text">' + value + '</span></td>');
            });
            $('#SubjectTable tbody').append(rowHTML);
        });
    }
}