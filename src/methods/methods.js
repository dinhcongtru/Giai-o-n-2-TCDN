export default {
     /**
         * Thực hiện fomat gender
         * Author : Đinh Công Trứ (30/10/2022)
         */
      fomatGender(gender){
        if(gender == 0){
            return 'Nam'
        }
        if(gender == 1){
            return 'Nữ'
        }
        if(gender == 2){
            return 'Khác'
        }

    },
    /**
         * Thực hiện xử lý dữ liệu DateOfBirth trả về từ api hiển thị ra bảng
         **  Author: Đinh Công Trứ(30/10/2022)
         */
         handleDOB(dob) {
            if (dob) {
                dob = new Date(dob);
                let date = dob.getDate(dob);
                let month = dob.getMonth(dob) + 1;
                let year = dob.getFullYear(dob);

                return `${date < 10 ? "0" + date : date}/${
                    month < 10 ? "0" + month : month
                }/${year}`;
            }
            return false;
        },
}