/**-------------------------------------------
 * Hàm chuyển format định dạng ngày tháng hiện cho người dùng
 * @param {Datetime} date
 * Author: quyetkaito (21/02/2022)
 ---------------------------------------------*/
 function formatDate(date, format) {
    try {
        if (!date) {
            return "";
        }
        var newDate = new Date(date);
        var dateString = newDate.getDate();
        if (dateString < 10) {
            dateString = "0" + dateString;
        }
        var monthString = newDate.getMonth() + 1;
        if (monthString < 10) {
            monthString = "0" + monthString;
        }
        var yearString = newDate.getFullYear();
        if (format) {
            if (format === "DD/MM/YYYY") {
                return `${dateString}/${monthString}/${yearString}`;
            }
            if (format === "MM/DD/YYYY") {
                return `${monthString}/${dateString}/${yearString}`;
            }
            if (format === "YYYY/MM/DD") {
                return `${yearString}/${monthString}/${dateString}`;
            }
        } else {
            return `${dateString}/${monthString}/${yearString}`;
        }
    } catch (error) {
        console.log(error);
    }
}

/**----------------------------------
 * Hàm set timezone cho ngày tháng
 * @param {Date} date 
 * Author: quyetkaito (18/03/2022)
 ----------------------------------*/
function setTimeZone(date) {
    try {
        if (date) {
            var newDate = date;
            newDate.setTime(
                newDate.getTime() - new Date().getTimezoneOffset() * 60 * 1000
            );
            return newDate.toISOString();
        } else {
            return "";
        }
    } catch (error) {
        console.log(error);
    }
}

/**--------------------------------------
     * Hàm format định dạng tiền tệ Việt Nam. => 123.123.123,4869
     * @param {String} money dạng chuỗi, có thể chứa cả dấu chấm dấu phẩy, ký tự chữ
     * @returns: kết quả dạng String.
     * Author: quyetkaito (27/03/2022)
     ----------------------------------------*/
function formatCurrency(money) {
    try {
        let moneyFormatted;
        //nếu money có giá trị
        if (money) {
            //nếu không phải là số (Not A Number)
            if (isNaN(money)) {
                //khi có 2 dấu '.' trở lên thì không phải là số nữa
                // loại bỏ kí tự không phải số
                money = money.replaceAll(".", "");
                //nếu có dấu phẩy => đang nhập số thập phân
                money = money.replaceAll(",", ".");
                money = money.replaceAll(/[^0-9.]/g, "").replace(/(\..*)\./g, "$1");
                //format lại
                moneyFormatted = new Intl.NumberFormat("vi-VN", {
                    minimumFractionDigits: 0,
                    maximumFractionDigits: 4, //phần thập phân tối đa 4 số
                }).format(money);
                return moneyFormatted;
            } else {
                //nếu đã là số rồi (có 1 dấu '.' vẫn là số)
                money = money.replaceAll(".", "");
                //format lại
                moneyFormatted = new Intl.NumberFormat("vi-VN", {
                    minimumFractionDigits: 0,
                    maximumFractionDigits: 4,
                }).format(money);
                return moneyFormatted;
            }
        } else {
            return "";
        }
    } catch (error) {
        console.log(error);
    }
}

/**---------------------------------------------------------------
 * Hàm convert lại giá trị tiền đã được format trở lại thành số.
 * @param {String} money : tiền đã được format dấu chấm dấu phẩy
 * @returns giá tiền dạng số
 * Author: quyetkaito (27/03/2022)
 -----------------------------------------------------------------*/
function formatCurrencyToSave(money) {
    try {
        if (money) {
            //bỏ hết dấu '.'
            money = money.replaceAll(".", "");
            //thay dấu ',' bằng dấu '.' để lấy phần thập phân
            money = money.replaceAll(",", ".");
            //convert về dạng số
            money = Number(money);
            return money;
        } else {
            return 0;
        }
    } catch (error) {
        console.log(error);
    }
}

/**------------------------------------------------
 * Hàm thực hiện format định dạng số 123.456,789
 * @param {Number} n - số tiền cần chuyển dạng số.
 * Author: quyetkaito (25/02/2022)
 --------------------------------------------------*/
function formatNumber(n) {
    return new Intl.NumberFormat("vi-VN").format(n);
}

/**-----------------------------------------------------------
 * Hàm thực hiện format định dạng số ngay trong khi nhập liệu.
 * @param {Event} event 
 * Author: quyetkaito (27/03/2022).
 -----------------------------------*/
function onNumberInput(event) {
    try {
        //lấy ra giá trị của ô input hiện tại
        let value = event.target.value;
        // console.log("ô input hiện tại: ", value);
        if (event.data == ",") {
            //nếu người dùng nhập dấu ',' và không có dấu ',' nào đã tồn tại => đang nhập số thập phân
            if ((value.match(/,/g) || []).length <= 1) {
                // console.log("chưa phải số thập phân");
                return; //cho nhập liệu tiếp
            }
        }
        //format số
        event.target.value = formatCurrency(value);
    } catch (error) {
        console.log(error);
    }
}

/**------------------------------------------
    *Hàm chuyển chuỗi camelCase sang snake_case
    @param {String} key-chuỗi đầu vào
    *Author: quyetkaito (07/04/2022)
    --------------------------------------------*/
function camelToUnderscore(key) {
    var result = key.replace(/([A-Z])/g, " $1");
    result = result.trim();
    return result.split(" ").join("_").toLowerCase();
}

/**------------------------------------------
*Hàm kiểm tra xem một chuỗi có phải là số hay không.
*@param {String} value: chuỗi kiểm tra
*Author: quyetkaito (17/04/2022)
--------------------------------------------*/
function isNumeric(value) {
    return /^-?\d+$/.test(value);
}

/**------------------------------------------
*Hàm so sánh hai ngày
*@param {Date} d1 ngày thứ nhất
*@param {Date} d2 ngày thứ 2
*@returns (-1: d1 nhỏ hơn d2), (0: d1==d2), (1: d1>d2)
*Author: quyetkaito (23/04/2022)
--------------------------------------------*/
function compareDate(d1, d2) {
    //lấy các giá trị ngày tháng năm.
    var date_1 = d1.getDate();
    var mon_1 = d1.getMonth();
    var y_1 = d1.getFullYear();
    var date_2 = d2.getDate();
    var mon_2 = d2.getMonth();
    var y_2 = d2.getFullYear();

    //so sánh năm
    if (y_1 > y_2) return 1;
    if (y_1 < y_2) return -1;
    if (y_1 == y_2) {
        //so sánh tháng
        if (mon_1 > mon_2) return 1;
        if (mon_1 < mon_2) return -1;
        if (mon_1 == mon_2) {
            //so sánh ngày
            if (date_1 > date_2) return 1;
            if (date_1 < date_2) return -1;
            if (date_1 == date_2) return 0;
        }
    }
}



export {
    formatDate,
    formatCurrency,
    formatNumber,
    setTimeZone,
    formatCurrencyToSave,
    onNumberInput,
    camelToUnderscore,
    isNumeric,
    compareDate
};