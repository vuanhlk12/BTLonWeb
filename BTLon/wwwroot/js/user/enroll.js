
$(document).ready(function () {
    var enroll = new Enroll();
});

class Enroll {
    constructor() {
        this.InitEvents();
        this.LoadData(this.GetData());
    }

    InitEvents() {
        //Subject
        $('#SubjectTable tbody').on('click', 'tr', this.SubjectTick);
        $('#SubjectRow').on('click', '#show', this.ShowDiaDiem.bind(this));

        //DiaDiem
        $('#DiaDiemTable tbody').on('click', 'tr', this.DiaDiemTick);
        $('#DiaDiemModal').on('click', '#save', this.AddSvDiaDiem.bind(this));
    }

    //Diadiem
    AddSvDiaDiem() {
        let me = this;
        let selectedDiaDiem = $('#DiaDiemTable .table-success')[0];
        let DiaDiemID = selectedDiaDiem.getAttribute('recordid');
        debugger
        $.ajax({
            method: 'Post',
            url: `/api/DiaDiems/AddUser/${DiaDiemID}`,
            async: false,
        });
        $('#DiaDiemModal').modal('hide');
    }

    DiaDiemTick() {
        $('#DiaDiemTable tr').removeClass('table-success');
        let row = this;
        $(row).addClass('table-success');
    }

    ShowDiaDiem() {
        let me = this;
        //me.OpenCaThi();
        $('#DiaDiemModal').modal('show');
        let monThiName = $('#SubjectTable .table-active [name=monThiName] span')[0].innerText;
        let giaoVien = $('#SubjectTable .table-active [name=giaoVien] span')[0].innerText;
        $('#DiaDiemModal .modal-title').text(`Môn: ${monThiName} - GV: ${giaoVien}`);
        let selected = $('#SubjectTable .table-active')[0];
        let MonThiID = selected.getAttribute('recordid');
        me.LoadDataDiaDiem(me.GetDataDiaDiem(MonThiID));
    }

    GetDataDiaDiem(MonThiID) {
        let data = [];
        $.ajax({
            method: 'GET',
            url: `/api/DiaDiems/GetByMonThi/${MonThiID}`,
            async: false,
            success: function (res) {
                data = res;
            },
            error: function (res) {
            }
        });
        return data;
    }

    LoadDataDiaDiem(data) {

        let fields = $('#DiaDiemTable th[name]');
        $('#DiaDiemTable tbody').empty();
        $.each(data, function (index, item) {
            let rowHTML = $('<tr recordID = "' + item['diaDiemId'] + '"></tr>');
            if (item['isValid'] == false) {
                rowHTML = $('<tr recordID = "' + item['diaDiemId'] + '" class="bg-danger"></tr>');
            }
            $.each(fields, function (fieldIndex, fieldItem) {
                let name = fieldItem.getAttribute('name');
                let value = item[name];
                if (name == 'date') {
                    let d = new Date(value);
                    value = d.formatddMMyyyy();
                }
                else if (name == 'start') {
                    let d = new Date(value);
                    value = d.getTime();
                }
                rowHTML.append('<td name="' + name + '"><span class="text">' + value + '</span></td>');
            });
            $('#DiaDiemTable tbody').append(rowHTML);
        });
    }

    //Subject
    OpenAddSubject() {
        $('#SubjectModal').modal('show');
    }

    SubjectTick() {
        $('#SubjectTable tr').removeClass('table-active');
        let row = this;
        if (!$(row).find('span').hasClass('badge-danger')) {
            $(row).addClass('table-active');
        }
    }

    GetData() {
        let data = [];
        $.ajax({
            method: 'GET',
            url: `/api/Subjects/UserMonThi`,
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
        debugger
        let fields = $('#SubjectTable th[name]');
        $('#SubjectTable tbody').empty();
        $.each(data, function (index, item) {
            let rowHTML = $('<tr recordID = "' + item['monThiId'] + '"></tr>');
            $.each(fields, function (fieldIndex, fieldItem) {
                let name = fieldItem.getAttribute('name');
                let value = item[name];
                if (name == 'isValid') {
                    if (item[name] == false) {
                        value = 'Không';
                        rowHTML.append('<td name="' + name + '"><span class="badge badge-danger">' + value + '</span></td>');
                    } else {
                        value = 'Có';
                        rowHTML.append('<td name="' + name + '"><span class="badge badge-success">' + value + '</span></td>');
                        debugger
                    }
                } else {
                    rowHTML.append('<td name="' + name + '"><span class="text">' + value + '</span></td>');}
            });
            $('#SubjectTable tbody').append(rowHTML);
        });
    }
}