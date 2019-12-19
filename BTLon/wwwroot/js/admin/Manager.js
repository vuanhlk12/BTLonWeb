
$(document).ready(function () {
    var manager = new Manager();
});

class Manager {
    constructor() {
        this.InitEvents();
        this.LoadDataKyThi(this.GetDataKyThi());
    }

    InitEvents() {
        //Ky thi
        $('#KyThiTable tbody').on('click', 'tr', this.KyThiTick);
        $('#KyThiRow').on('click', '#show', this.ShowCaThi.bind(this));
        $('#KyThiRow').on('click', '#add', this.OpenAddKyThi.bind(this));
        $('#KyThiRow').on('click', '#delete', this.DeleteKyThi.bind(this));
        $('#KyThiRow').on('click', '#set', this.SetCurrentKyThiForAllUsers.bind(this));
        $('#KyThiModal').on('click', '#save', this.AddKyThi.bind(this));

        //Ca thi
        $('#CaThiTable tbody').on('click', 'tr', this.CaThiTick);
        $('#CaThiRow').on('click', '#show', this.ShowSubject.bind(this));
        $('#CaThiRow').on('click', '#back', this.ShowKyThi.bind(this));
        $('#CaThiRow').on('click', '#add', this.OpenAddCaThi.bind(this));
        $('#CaThiRow').on('click', '#delete', this.DeleteCaThi.bind(this));
        $('#CaThiModal').on('click', '#save', this.AddCaThi.bind(this));

        //Mon thi
        $('#SubjectTable tbody').on('click', 'tr', this.SubjectTick);
        $('#SubjectRow').on('click', '#show', this.ShowPhongThi.bind(this));
        $('#SubjectRow').on('click', '#back', this.ShowCaThi.bind(this));
        $('#SubjectRow').on('click', '#add', this.OpenAddMonThi.bind(this));
        $('#SubjectRow').on('click', '#delete', this.DeleteCtMt.bind(this));
        $('#MonThiModal').on('click', '#save', this.AddCtMt.bind(this));

        //Mon thi trong modal
        $('#SubjectTableInModal tbody').on('click', 'tr', this.SubjectModalTick);

        //Phong thi
        $('#ClassroomTable tbody').on('click', 'tr', this.PhongThiTick);
        $('#ClassroomRow').on('click', '#back', this.ShowSubject.bind(this));
        $('#ClassroomRow').on('click', '#show', this.ShowSinhVien.bind(this));
        $('#ClassroomRow').on('click', '#add', this.OpenAddPhongThi.bind(this));
        $('#ClassroomRow').on('click', '#delete', this.DeleteDiaDiem.bind(this));
        $('#PhongThiModal').on('click', '#save', this.AddDiaDiem.bind(this));

        //Phong thi trong modal
        $('#ClassroomTableInModal tbody').on('click', 'tr', this.ClassroomModalTick);


    }

    //Ky Thi
    SetCurrentKyThiForAllUsers() {
        let me = this;
        let KTselected = $('#KyThiTable .table-active')[0];
        let KyThiID = KTselected.getAttribute('recordid'); 
        $.ajax({
            method: 'put',
            url: '/api/KyThis/SetKyThi/' + KyThiID,
            async: false
        });
    }

    DeleteKyThi() {
        let me = this;
        let KTselected = $('#KyThiTable .table-active')[0];
        let KyThiID = KTselected.getAttribute('recordid');
        $.ajax({
            method: 'delete',
            url: '/api/KyThis/' + KyThiID,
            async: false
        });
        this.LoadDataKyThi(this.GetDataKyThi());
    }

    AddKyThi() {
        let me = this;
        let object = {};
        object['kyThiIdFake'] = $('#KyThiModal [name=kyThiIdFake]').val();
        object['KyThiName'] = $('#KyThiModal [name=KyThiName]').val();
        $.ajax({
            method: 'Post',
            url: '/api/KyThis/',
            data: JSON.stringify(object),
            async: false,
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        });
        $('#KyThiModal').modal('hide');
        this.LoadDataKyThi(this.GetDataKyThi());
    }

    OpenAddKyThi() {
        $('#KyThiModal').modal('show');
    }

    ShowKyThi() {
        this.OpenKyThi();
        $('#directory').empty();
        $('#directory').append($('<li>Quản lý</li>'));
        this.LoadDataKyThi(this.GetDataKyThi());
    }

    OpenKyThi() {
        $('.row').hide();
        $('#KyThiRow').show();
    }

    KyThiTick() {
        $('#KyThiTable tr').removeClass('table-active');
        let row = this;
        $(row).addClass('table-active');
    }

    GetDataKyThi() {
        let data = [];
        $.ajax({
            method: 'GET',
            url: '/api/KyThis',
            async: false,
            success: function (res) {
                data = res;
            },
            error: function (res) {
            }
        });

        return data;

    }

    LoadDataKyThi(data) {
        var fields = $('#KyThiTable th[name]');
        $('#KyThiTable tbody').empty();
        $.each(data, function (index, item) {
            var rowHTML = $('<tr recordID = "' + item['kyThiId'] + '"></tr>');
            $.each(fields, function (fieldIndex, fieldItem) {
                var name = fieldItem.getAttribute('name');
                var value = item[name];
                rowHTML.append('<td name="' + name + '"><span class="text">' + value + '</span></td>');
            });
            $('#KyThiTable tbody').append(rowHTML);
        });
    }

    //Ca Thi
    DeleteCaThi() {
        let me = this;
        let KTselected = $('#KyThiTable .table-active')[0];
        let KyThiID = KTselected.getAttribute('recordid');
        let selected = $('#CaThiTable .table-active')[0];
        let CaThiID = selected.getAttribute('recordid');
        $.ajax({
            method: 'delete',
            url: '/api/CaThis/' + CaThiID,
            async: false
        });
        me.LoadDataCaThi(me.GetDataCaThi(KyThiID));
    }

    AddCaThi() {
        let me = this;
        let object = {};
        let selected = $('#KyThiTable .table-active')[0];
        let KyThiID = selected.getAttribute('recordid');
        object['KyThiID'] = KyThiID;
        object['CaThiIdFake'] = $('#CaThiModal [name=CaThiIdFake]').val();
        object['CaThiName'] = $('#CaThiModal [name=CaThiName]').val();
        object['Date'] = new Date($('#CaThiModal [name=Date]').val());
        let start = $('#CaThiModal [name=Date]').val() + ' ' + (parseInt($('#CaThiModal [name=start] [name=hour]').val()) + 7) + ':' + $('#CaThiModal [name=start] [name=minute]').val();
        object['Start'] = new Date(start);
        let stop = $('#CaThiModal [name=Date]').val() + ' ' + (parseInt($('#CaThiModal [name=stop] [name=hour]').val()) + 7) + ':' + $('#CaThiModal [name=stop] [name=minute]').val();
        object['Stop'] = new Date(stop);
        $.ajax({
            method: 'Post',
            url: '/api/CaThis/',
            data: JSON.stringify(object),
            async: false,
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        });
        $('#CaThiModal').modal('hide');
        me.LoadDataCaThi(me.GetDataCaThi(KyThiID));
    }

    OpenAddCaThi() {
        $('#CaThiModal').modal('show');
    }

    OpenCaThi() {
        $('.row').hide();
        $('#CaThiRow').show();

    }

    ShowCaThi() {
        let me = this;
        me.OpenCaThi();
        let selected = $('#KyThiTable .table-active')[0];
        let KyThiID = selected.getAttribute('recordid');
        let KyThiName = $('#KyThiTable .table-active [name=kyThiName] span')[0].innerText;
        $('#directory').empty();
        $('#directory').append($('<li>Quản lý</li>' + '<li>/</li>' + `<li>${KyThiName}</li>`));
        me.LoadDataCaThi(me.GetDataCaThi(KyThiID));
    }

    CaThiTick() {
        $('#CaThiTable tr').removeClass('table-active');
        let row = this;
        $(row).addClass('table-active');
    }

    GetDataCaThi(KyThiID) {

        let data = [];
        $.ajax({
            method: 'GET',
            url: '/api/CaThis/' + KyThiID,
            async: false,
            success: function (res) {
                data = res;
            },
            error: function (res) {
            }
        });

        return data;

    }

    LoadDataCaThi(data) {
        var fields = $('#CaThiTable th[name]');
        $('#CaThiTable tbody').empty();
        $.each(data, function (index, item) {
            var rowHTML = $('<tr recordID = "' + item['caThiId'] + '"></tr>');

            $.each(fields, function (fieldIndex, fieldItem) {
                var name = fieldItem.getAttribute('name');
                var value = item[name];
                if (name == 'date') {
                    let d = new Date(value);
                    value = d.formatddMMyyyy();
                }
                else if (name == 'start' || name == 'stop') {
                    let d = new Date(value);
                    value = d.getTime();
                }
                rowHTML.append('<td name="' + name + '"><span class="text">' + value + '</span></td>');
            });
            $('#CaThiTable tbody').append(rowHTML);
        });
    }

    //Mon hoc
    AddCaThi() {
        let me = this;
        let object = {};
        let selected = $('#KyThiTable .table-active')[0];
        let KyThiID = selected.getAttribute('recordid');
        object['KyThiID'] = KyThiID;
        object['CaThiIdFake'] = $('#CaThiModal [name=CaThiIdFake]').val();
        object['CaThiName'] = $('#CaThiModal [name=CaThiName]').val();
        object['Date'] = new Date($('#CaThiModal [name=Date]').val());
        let start = $('#CaThiModal [name=Date]').val() + ' ' + (parseInt($('#CaThiModal [name=start] [name=hour]').val()) + 7) + ':' + $('#CaThiModal [name=start] [name=minute]').val();
        object['Start'] = new Date(start);
        let stop = $('#CaThiModal [name=Date]').val() + ' ' + (parseInt($('#CaThiModal [name=stop] [name=hour]').val()) + 7) + ':' + $('#CaThiModal [name=stop] [name=minute]').val();
        object['Stop'] = new Date(stop);
        $.ajax({
            method: 'Post',
            url: '/api/CaThis/',
            data: JSON.stringify(object),
            async: false,
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        });
        $('#CaThiModal').modal('hide');
        me.LoadDataCaThi(me.GetDataCaThi(KyThiID));
    }

    OpenAddMonThi() {
        $('#MonThiModal').modal('show');
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
        let fields = $('#MonThiModal #SubjectTableInModal th[name]');
        $('#MonThiModal #SubjectTableInModal tbody').empty();
        $.each(data, function (index, item) {
            let rowHTML = $('<tr recordID = "' + item['monThiId'] + '"></tr>');
            $.each(fields, function (fieldIndex, fieldItem) {
                let name = fieldItem.getAttribute('name');
                let value = item[name];
                rowHTML.append('<td name="' + name + '"><span class="text">' + value + '</span></td>');
            });
            $('#MonThiModal #SubjectTableInModal tbody').append(rowHTML);
        });
    }

    OpenSubject() {
        $('.row').hide();
        $('#SubjectRow').show();
    }

    ShowSubject() {
        let me = this;
        me.OpenSubject();
        let selected = $('#CaThiTable .table-active')[0];
        let CaThiID = selected.getAttribute('recordid');
        let KyThiName = $('#KyThiTable .table-active [name=kyThiName] span')[0].innerText;
        let CaThiName = $('#CaThiTable .table-active [name=caThiName] span')[0].innerText;
        $('#directory').empty();
        $('#directory').append($('<li>Quản lý</li>' + '<li>/</li>' + `<li>${KyThiName}</li>` + '<li>/</li>' + `<li>${CaThiName}</li>`));
        me.LoadDataSubject(me.GetDataSubject(CaThiID));
    }

    SubjectTick() {
        $('#SubjectTable tr').removeClass('table-active');
        let row = this;
        $(row).addClass('table-active');
    }

    GetDataSubject(CaThiID) {
        let data = [];
        $.ajax({
            method: 'GET',
            url: '/api/Subjects/' + CaThiID,
            async: false,
            success: function (res) {
                data = res;
            },
            error: function (res) {
            }
        });

        return data;

    }

    LoadDataSubject(data) {
        var fields = $('#SubjectTable th[name]');

        $('#SubjectTable tbody').empty();

        $.each(data, function (index, item) {
            var rowHTML = $('<tr recordID = "' + item['monThiId'] + '"></tr>');

            $.each(fields, function (fieldIndex, fieldItem) {
                var name = fieldItem.getAttribute('name');
                var value = item[name];
                rowHTML.append('<td name="' + name + '"><span class="text">' + value + '</span></td>');

            });
            $('#SubjectTable tbody').append(rowHTML);

        });
    }

    //Mon thi - Ca thi
    SubjectModalTick() {
        $('#SubjectTableInModal tr').removeClass('table-success');
        let row = this;
        $(row).addClass('table-success');
    }

    AddCtMt() {
        let me = this;
        let selectedMT = $('#SubjectTableInModal .table-success')[0];
        let MonThiID = selectedMT.getAttribute('recordid');
        let selectedCT = $('#CaThiTable .table-active')[0];
        let CaThiID = selectedCT.getAttribute('recordid');
        let object = {};
        object['MonThiID'] = MonThiID;
        object['CaThiID'] = CaThiID;
        $.ajax({
            method: 'Post',
            url: '/api/CtMts/',
            data: JSON.stringify(object),
            async: false,
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        });
        $('#MonThiModal').modal('hide');
        me.LoadDataSubject(me.GetDataSubject(CaThiID));
    }

    DeleteCtMt() {
        let me = this;
        let selectedMT = $('#SubjectTable .table-active')[0];
        let MonThiID = selectedMT.getAttribute('recordid');
        let selectedCT = $('#CaThiTable .table-active')[0];
        let CaThiID = selectedCT.getAttribute('recordid');
        let object = {};
        object['MonThiID'] = MonThiID;
        object['CaThiID'] = CaThiID;
        $.ajax({
            method: 'delete',
            url: '/api/CtMts/',
            data: JSON.stringify(object),
            async: false,
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        });
        me.LoadDataSubject(me.GetDataSubject(CaThiID));
    }

    //Phong thi

    OpenAddPhongThi() {
        $('#PhongThiModal').modal('show');
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
        let fields = $('#PhongThiModal #ClassroomTableInModal th[name]');
        $('#PhongThiModal #ClassroomTableInModal tbody').empty();
        $.each(data, function (index, item) {
            let rowHTML = $('<tr recordID = "' + item['phongThiId'] + '"></tr>');
            $.each(fields, function (fieldIndex, fieldItem) {
                let name = fieldItem.getAttribute('name');
                let value = item[name];
                rowHTML.append('<td name="' + name + '"><span class="text">' + value + '</span></td>');
            });
            $('#PhongThiModal #ClassroomTableInModal tbody').append(rowHTML);
        });
    }

    PhongThiTick() {
        $('#ClassroomTable tr').removeClass('table-active');
        let row = this;
        $(row).addClass('table-active');
    }

    OpenPhongThi() {
        $('.row').hide();
        $('#ClassroomRow').show();
    }

    ShowPhongThi() {
        let me = this;
        me.OpenPhongThi();
        let selectedSubject = $('#SubjectTable .table-active')[0];
        let SubjectID = selectedSubject.getAttribute('recordid');
        let selectedCaThi = $('#CaThiTable .table-active')[0];
        let CaThiID = selectedCaThi.getAttribute('recordid');


        let KyThiName = $('#KyThiTable .table-active [name=kyThiName] span')[0].innerText;
        let CaThiName = $('#CaThiTable .table-active [name=caThiName] span')[0].innerText;
        let MonThiName = $('#SubjectTable .table-active [name=monThiName] span')[0].innerText;
        $('#directory').empty();
        $('#directory').append($('<li>Quản lý</li>' + '<li>/</li>' + `<li>${KyThiName}</li>` + '<li>/</li>' + `<li>${CaThiName}</li>` + '<li>/</li>' + `<li>${MonThiName}</li>`));


        me.LoadDataClassrooms(me.GetDataClassroom(CaThiID, SubjectID));
    }

    GetDataClassroom(CaThiID, SubjectID) {
        let data = [];
        $.ajax({
            method: 'GET',
            url: '/api/Classrooms/' + CaThiID + '/' + SubjectID,
            async: false,
            success: function (res) {
                data = res;
            },
            error: function (res) {
            }
        });
        return data;


    }

    LoadDataClassrooms(data) {
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

    //Dia Diem

    ClassroomModalTick() {
        $('#ClassroomTableInModal tr').removeClass('table-success');
        let row = this;
        $(row).addClass('table-success');
    }

    AddDiaDiem() {
        let me = this;
        let selectedSubject = $('#SubjectTable .table-active')[0];
        let SubjectID = selectedSubject.getAttribute('recordid');
        let selectedCaThi = $('#CaThiTable .table-active')[0];
        let CaThiID = selectedCaThi.getAttribute('recordid');
        let selectedPT = $('#ClassroomTableInModal .table-success')[0];
        let PhongThiID = selectedPT.getAttribute('recordid');
        $.ajax({
            method: 'Post',
            url: '/api/DiaDiems/' + CaThiID + '/' + SubjectID + '/' + PhongThiID,
            async: false,
        });
        $('#PhongThiModal').modal('hide');
        me.LoadDataClassrooms(me.GetDataClassroom(CaThiID, SubjectID));
    }

    DeleteDiaDiem() {
        let me = this;
        let selectedMT = $('#SubjectTable .table-active')[0];
        let MonThiID = selectedMT.getAttribute('recordid');
        let selectedCT = $('#CaThiTable .table-active')[0];
        let CaThiID = selectedCT.getAttribute('recordid');
        let selectedPT = $('#ClassroomTable .table-active')[0];
        let PhongThiID = selectedPT.getAttribute('recordid');
        $.ajax({
            method: 'delete',
            url: '/api/DiaDiems/' + CaThiID + '/' + MonThiID + '/' + PhongThiID,
            async: false,
        });
        me.LoadDataClassrooms(me.GetDataClassroom(CaThiID, MonThiID));
    }

    //Sinh vien
    ShowSinhVien() {
        let me = this;
        $('#SinhVienModal').modal('show');
        let SubjectID = $('#SubjectTable .table-active')[0].getAttribute('recordid');
        let CaThiID = $('#CaThiTable .table-active')[0].getAttribute('recordid');
        let PhongThiID = $('#ClassroomTable .table-active')[0].getAttribute('recordid');
        me.LoadDataSinhVien(me.GetDataSinhVien(CaThiID, SubjectID, PhongThiID));

    }

    GetDataSinhVien(CaThiID, SubjectID, PhongThiID) {
        let data = [];
        $.ajax({
            method: 'GET',
            url: `/api/Sv/GetByDiaDiem/${CaThiID}/${SubjectID}/${PhongThiID}`,
            async: false,
            success: function (res) {
                data = res;
            },
            error: function (res) {
            }
        });
        return data;


    }

    LoadDataSinhVien(data) {
        var fields = $('#SinhVienTableInModal th[name]');
        $('#SinhVienTableInModal tbody').empty();
        $.each(data, function (index, item) {
            var rowHTML = $('<tr recordID = "' + item['phongThiId'] + '"></tr>');
            $.each(fields, function (fieldIndex, fieldItem) {
                var name = fieldItem.getAttribute('name');
                var value = item[name];
                if (name == 'birth') {
                    let d = new Date(value);
                    value = d.formatddMMyyyy();
                }
                rowHTML.append('<td name="' + name + '"><span class="text">' + value + '</span></td>');

            });
            $('#SinhVienTableInModal tbody').append(rowHTML);
        });
    }
}
