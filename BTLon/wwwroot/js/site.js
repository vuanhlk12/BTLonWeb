// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

Date.prototype.formatddMMyyyy = function () {
    var day = this.getDate() + "";
    if (day.length == 1) { day = "0" + day };
    var month = this.getMonth() + 1 + "";

    if (month.length == 1) { month = "0" + month };
    var year = this.getFullYear();
    return year + '-' + month + '-' + day;
}

Date.prototype.getTime = function () {
    let hour = this.getHours() + "";
    if (hour.length == 1) { hour = "0" + hour };
    var min = this.getMinutes() + "";
    if (min.length == 1) { min = "0" + min };
    return `${hour}:${min}`;
}

Date.prototype.addHours = function (h) {
    this.setHours(this.getHours() + h);
    return this;
}