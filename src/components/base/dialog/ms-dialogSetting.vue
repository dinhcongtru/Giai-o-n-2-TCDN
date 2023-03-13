<template>
    <ms-dialog-container ref="settingDialog" :isRight="true">
      <!-- Header của dialog setting -->
      <template v-slot:header>
        <div class="m-dialog-header">
          <div class="m-dialog-header__left">
            <div class="title-container">
              <div class="dialog-title-text">{{ formTitle }}</div>
            </div>
          </div>
          <div class="m-dialog-header__right">
            <div style="position: relative">
              <div
                class="mi-help"
                style="margin-right: 6px"
              ></div>
              <ms-tooltip class="hide" text="Giúp (F1)" :isTop="true" />
            </div>
            <div style="position: relative">
              <div class="mi-close" @click="closeDialog()"></div>
              <ms-tooltip class="hide" text="Đóng (ESC)" :isTop="true" />
            </div>
          </div>
        </div>
      </template>
      <!-- Content của dialog -->
      <template v-slot:content>
        <div class="flex pb-10 justify-spacebetween">
          <div class="w-1/4">
            <div class="row-right">
                <ms-input type="text" 
                :autoFocus="true" 
                :isHasLabel="false" 
                placeholder="Nhập từ khóa tìm kiếm" 
                id="search-employee"  
                styleClass="input-search" 
                ref="search" 
                v-model="filter" 
                autocomplete="off"/>
                <span class="icon-search-setting"></span>
            </div>
         
            
          </div>
          <div class="w-1/2 flex justify-end">
            <div class="show-setting-text flex align-y-center">
              Sửa tên cột hiển thị và độ rộng
            </div>
          </div>
        </div>
        <div class="form-detail form-detail-setting">
          <ms-grid-editable
            ref="mGridEditable"
            :listHeader="listHeader"
            :listData="listData"
            :isCheckable="true"
            :isChangeRow="false"
            :isHasFunctionColumn="false"
            :isBorderBottom="true"
            @eCheckRow="getRow"
          />
        </div>
      </template>
      <!-- //Content của dialog -->
  
      <!-- Footer của dialog setting -->
      <template v-slot:footer>
        <div class="footer-container">
          <div class="footer-line">
            <button class="m-btn-setting" @click="closeDialog">Hủy</button>
            <div class="group-btn-popup align-right">
              <div style="position: relative">
                <button
                  class="m-btn-setting"
                  @click="getDefaultTemplate()"
                >
                  Lấy mẫu ngầm định
                </button>
              </div>
              <div style="position: relative">
                <button
                  class="m-btn-settingSave"
                  style="margin-left: 10px"
                  @click="saveColumn()"
                >
                  Cất
                </button>
              </div>
            </div>
          </div>
        </div>
      </template>
    </ms-dialog-container>
  </template>
  <script>
  import msDialogContainer from './ms-dialogContainer.vue';
  import msInput from "@/components/base/input/ms-input.vue";
  // import msTooltip from '@/components/base/message/message/ms-ToolTip.vue';
  // import msTooltipVue from "@/components/base/ms-toolTip.vue"
  export default {
    components:{msDialogContainer,msInput},
    emits: ["eUpdateColumn", "eGetDefaultTemplate"],
    props: {
      //header của bảng tùy chỉnh giao diện (fix luôn giá trị mặc định), sau muốn truyền vào thì truyền
      listHeader: {
        type: Array,
        default: () => [
          {
            text: "Tên cột dữ liệu",
            fieldName: "ColumnName",
            align:"justify-start",
            type: "text",
          },
          {
            text: "Tên cột trên giao diện",
            fieldName: "DisplayColumnName",
            type: "input",
            columnWidth:200
          },
          {
            text: "Độ rộng",
            fieldName: "ColumnWidth",
            type: "input",
            columnWidth:200
          },
          // {
          //   text: "Cố định cột",
          //   fieldName: "IsFixedColumn",
          //   align: "text-align-center justify-center",
          //   type: "checkbox",
          // },
        ],
      },
  
      //list Data của bảng tùy chỉnh giao diện => để render ra list các header bảng ngoài (lấy từ server về)
      listData: {
        type: Array, //là một object chứa các thông tin cột, tên, tên hiển thị, độ rộng, có cố định hay không
        default: () => [
          // {
          //   Show:true,
          //   ColumnName: 'Mã nhà cung cấp',
          //   DisplayColumnName: 'Mã nhà cung cấp',
          //   ColumnWidth: '200',
          //   IsFixedColumn: true,
          //   FieldName: 'account_object_code',
          //   DataFormat: 0-text, 1-input, 2-inputnumber, 3- checkbox, 4- selectbox, 5-ngày tháng
          // },        
        ],
      },
    },
    data() {
      return {
        //tiêu đề của dialog
        formTitle: "Tùy chỉnh giao diện",
        listColumn: [], //danh sách các cột của bảng bên ngoài
        listTmp:[],//lưu tạm danh sách các cột được check từ grid-edittable
      };
    },
    methods: {
      /**------------------------------------------
      * Xử lý khi nút [Lấy mẫu ngầm định được click]
      * Author: quyetkaito (12/04/2022)
      --------------------------------------------*/
      getDefaultTemplate() {
        this.$emit("eGetDefaultTemplate");
      },
  
      /**-----------------------------------------------------------------
       * Nhận list hàng được chọn từ grid selectbox
       * @param {Array} listRow: danh sách các hàng đang tích (từ grid truyền lên)
       * Author: quyetkaito (01/04/2022)
      --------------------------------------------------------------------*/
      getRow(listRow) {
        try {
          this.listTmp = this.listData;
          //sắp xếp listRow theo đúng thứ tự ban đầu
          for (const i of this.listTmp) {
            //duyệt trong list các hàng được check
            let flag = false; //biến cờ xác định phần tử có trong list hay không.
            for (const j of listRow) {
              //nếu tìm thấy hàng được chọn có trong danh sách
              if (i.ColumnName == j.ColumnName) {
                i.Show = true; //hiển thị = true
                flag = true;
                break;
              }
            }
            //chạy hết vòng for mà flag vẫn bằng false => không có trong list
            if (flag == false) {
              i.Show = false; //hiển thị bằng false
            }
          }
  
          //kết quả trả về là this.listTmp có đủ các cột ban đầu 
          //và thuộc tính Show = true/false giống các hàng trong grid.
        } catch (error) {
          //log ra lỗi
          console.log(error);
        }
      },
  
      /**--------------------------------------------------
       * Thực hiện lưu lại danh sách các cột đang setting.
       * Author: quyetkaito (01/04/2022)
      ----------------------------------------------------*/
      saveColumn() {
        try {
          //gán danh sách các cột bằng danh sách đã sắp xếp bên trên.
          this.listColumn = this.listTmp;
          //emit tới cha danh sách các column
          this.$emit("eUpdateColumn", this.listColumn);
          //đóng setting dialog
          this.closeDialog();
        } catch (error) {
          console.log(error);
        }
      },
  
      /**----------------------------------
         * Hàm gọi tới dialogContainer show.
         * Author: quyetkaito (30/03/2022)
         ------------------------------------*/
      closeDialog() {      
        this.$refs.settingDialog.hide();
      },
  
      /**----------------------------------
         * Hàm gọi tới dialogContainer hide.
         * Author: quyetkaito (30/03/2022)
         ------------------------------------*/
      show() {
        this.$refs.settingDialog.show();
      },
    },
  };
  </script>
  
  <style>
  .show-setting-text {
    font-size: 13px;
    color: #0075c0;
    cursor: pointer;
  }
  .form-detail.form-detail-setting{
    height: calc(100vh - 73px - 42px - 87px);
  }
  .group-btn-popup {
    padding: 0;
    display: flex;
}
.m-btn-setting{
    outline: none;
    border: 1px solid #8d9096;
    color: #111;
    background-color: transparent;
    border-radius: 3px;
    padding: 6px 16px;
    font-weight: 600;
    cursor: pointer;
}
.m-btn-setting:hover{
    background: #d2d3d6;
}
.m-btn-settingSave{
    outline: none;
    border: 1px solid transparent;
    color: #fff;
    background-color: #2ca01c;
    border-radius: 3px;
    padding: 6px 16px;
    font-weight: 600;
    cursor: pointer;
}
.m-btn-settingSave:hover{
    background: #35bf22;
    color: #fff;
}

  </style>