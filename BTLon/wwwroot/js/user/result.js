
$(document).ready(function () {
    var result = new Result();
});

class Result {
    constructor() {
        this.InitEvents();
        this.LoadData(this.GetData());
    }

    InitEvents() {
        //Subject
        //$('#SubjectTable tbody').on('click', 'tr', this.SubjectTick);
        //$('#SubjectRow').on('click', '#show', this.ShowDiaDiem.bind(this));

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
            url: `/api/DiaDiems/GetDetail`,
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

        let fields = $('#KetQuaTable th[name]');
        $('#KetQuaTable tbody').empty();
        $.each(data, function (index, item) {
            let rowHTML = $('<tr recordID = "' + item['monThiId'] + '"></tr>');
            if (item['isValid'] == false) {
                rowHTML = $('<tr recordID = "' + item['monThiId'] + '" class="bg-danger"></tr>');
            }
            $.each(fields, function (fieldIndex, fieldItem) {
                let name = fieldItem.getAttribute('name');
                let value = item[name];
                if (name == 'date') {
                    let d = new Date(value);
                    value = d.formatddMMyyyy();
                }
                else if (name == 'start') {
                    debugger
                    let d = new Date(value);
                    value = d.getTime();
                }
                rowHTML.append('<td name="' + name + '"><span class="text">' + value + '</span></td>');
            });
            $('#KetQuaTable tbody').append(rowHTML);
        });
    }
}