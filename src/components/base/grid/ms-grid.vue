<template>
    <table ref="mTable" class="table">
      <!-- Table Header -->
      <thead v-if="isHasHeader">
        <tr>
          <th v-if="isDragable" class="w-50"></th>
          <th v-if="isCheckable">
            <div class="m-checkbox">
              <input type="checkbox" v-model="selectAll" ref="selectAll" />
            </div>
          </th>
          <!-- Lấy tên các header để render ra -->
          <th
            v-for="(header, index) in listHeader"
            :key="index"
            :style="`width:${header.columnWidth}px!important;min-width:${header.columnWidth}px!important;`"
          >
            <div :title="header.textFull" :class="[header.align]">
              {{ header.text }}
            </div>
          </th>
          <th v-if="isHasFunctionColumn">
            <div class="justify-end">chức năng</div>
          </th>
        </tr>
      </thead>
  
      <!-- Table Body -->
      <tbody v-if="listData.length !== 0">
        <tr
          v-for="(object, index) in listData"
          :key="index"
          class="trGroupVendor"

          @dblclick="handleEdit(object)"
          @click="handleClick(object, index)"
        >
          <td v-if="isDragable" class="w-50">
            <div class="m-icon m-icon-16 m-drag-grid"></div>
          </td>
          <td v-if="isCheckable" class="w-50">
            <div class="m-checkbox">
              <input
                type="checkbox"
                v-model="checkedRows"
                :value="object"
                @change="handleCheckboxChange"
              />
            </div>
          </td>
          <td
            v-for="(column, idx) in listHeader"
            :key="idx"
            :style="`width:${column.columnWidth}px!important;min-width:${column.columnWidth}px!important;max-width:300px!important;`"
          >
            <div :class="column.align">
              <!-- {{ column.columnWidth }} -->
              {{ showData(object, column.fieldName) }}
            </div>
          </td>
          <td v-if="isHasFunctionColumn">
            <!-- table function -->
            <div class="m-table-tool flex align-y-center justify-end">
              <div class="m-table-tool__text" @click="handleEdit(object)">
                <button class="tool-btn">{{ functionText }}</button>
              </div>
              <div ref="dropdownArrow" class="m-table-tool__icon">
                <div
                  class="line flex align-center"
                  @click="showOptions($event, object, index)"
                  @doubleClick="handleEdit(object)"
                >
                  <div class="m-icon m-icon-16 m-arrow-up--blue"></div>
                </div>
              </div>
            </div>
            <!-- /table function -->
          </td>
        </tr>
      </tbody>
      <!-- Table body skeleton -->
      <tbody v-if="isShowSkeleton" class="empty">
        <tr v-for="i in pageSize" v-bind:key="i">
          <td v-for="col in columnSkeleton" :key="col"><div></div></td>
        </tr>
      </tbody>
  
      <!-- Table Footer -->
      <tfoot v-if="showFooter && listData.length !== 0" class="m-table-footer">
        <tr>
          <th v-if="isDragable" class="w-50"></th>
          <th v-if="isCheckable" class="w-50"></th>
          <th
            v-for="(header, index) in listHeader"
            :key="index"
            :style="`width:${header.columnWidth}px!important;min-width:${header.columnWidth}px!important;`"
          >
            <div v-if="index == 0" class="align-center">Tổng</div>
            <div :class="header.align" v-if="index != 0">
              {{ header.total_amount }}
            </div>
          </th>
          <th v-if="isHasFunctionColumn"></th>
        </tr>
      </tfoot>
    </table>
  </template>
  
  <script>
  
  import * as base from "@/js/base";
  import Enum from "@/js/enum";
  export default {
    component: {},
    emits: [
      "onUpdate",
      "onDelete",
      "onDblClick",
      "eClick",
      "eShowFunc",
      "handleHideFunc",
      "eMultiSelect",
    ],
   
  
    //danh sách các prop
    props: {
      //list data render ra bảng
      listData: {
        type: Object,
        default: null,
        required: true, //bắt buộc có
      },
      //list các header của bảng
      listHeader: {
        type: Array,
        default: null,
        required: true, //bắt buộc
      },
      //số bản ghi trên một trang
      pageSize: {
        type: [Number, String],
        default: null,
      },
      //loading skeleton có show hay không?
      isShowSkeleton: {
        type: Boolean,
        default: false,
      },
      //text của button function
      functionText: {
        type: String,
        default: "function-btn",
      },
      //có cột checkbox hay không?
      isCheckable: {
        type: Boolean,
        default: true,
      },
      //có cột chức năng hay không?
      isHasFunctionColumn: {
        type: Boolean,
        default: true,
      },
      //các hàng có thể kéo thả để thay đổi vị trí không?
      isDragable: {
        type: Boolean,
        default: false,
      },
      //bảng này có hiện header không?
      isHasHeader: {
        type: Boolean,
        default: true,
      },
      //footer có show không?
      showFooter: {
        type: Boolean,
        default: false,
      },
      //icon loading có show không?
      isShowLoading: {
        type: Boolean,
        default: false,
      },
    },
  
    //danh sách các data
    data() {
      return {
        isCheckAll: false, //nút check all có đang check hay không?
        checkedRows: [], //số hàng đang được chọn => (mảng các object)
      };
    },
    mounted() {},
    watch: {
      /**----------------------------------------------------------
       * nếu checkedRows thay đổi thì cập nhật lại các hàng dc select
       * Author: quyetkaito (14/03/2022)
       ---------------------------------*/
      checkedRows() {
        this.changeSelected();
      },
  
      /**--------------------------------------
       * Nếu đang hiện skeleton thì bỏ check tất cả.
       * Author: quyetkaito (22/-3/2022)
       ---------------------------------*/
      isShowSkeleton() {
        this.checkedRows = [];
      },
    },
  
    //các hàm
    methods: {
      /**---------------------------------------------
       * Thực hiện bỏ dấu checkAll sau khi xóa nhiều.
       * Author: quyetkaito (18/03/2022)
       ---------------------------------*/
      unCheckAll() {
        if (this.$refs.selectAll.checked == true)
          this.$refs.selectAll.checked = false;
      },
  
      /**-----------------------------------
       * Hàm show data tương ứng với các cột của header.
       * @param {String} object - object hàng hiện tại.
       * @param {String} key - tên cột.
       * Author: quyetkaito (16/03/2022)
       ------------------------------------*/
      showData(object, key) {
        //chuyển column fieldName sang snake_case (nếu cần)
        let field = base.camelToUnderscore(key);
  
        //nếu dữ liệu dạng số
        if (typeof object[field] == "number") {
          return base.formatCurrency(object[field].toString());
        }
  
        // //nếu có thể convert thành dạng ngày và đầu vào không phải là số.
        // let date = new Date(object[field]);
        // if((date.getDate()||date.getMonth()||date.getFullYear()) && !isNumeric(object[field])){
        //   return formatDate(object[field], Enum.DateFormat);
        // }
  
        if (
          field == "date_of_birth" ||
          field == "identity_date" ||
          field == "posted_date" ||
          field == "ref_date"
        ) {
          return base.formatDate(object[field], Enum.DateFormat);
        }
  
        //nếu không => trả về data với field tương ứng từ header(dạng text bình thường)
        return object[field];
      },
  
      /**----------------------------------------------
       * Hàm emit tới cha danh sách các hàng được chọn.
       * Author: quyetkaito (15/03/2022)
      ------------------------------------*/
      changeSelected() {
        // console.log(this.checkedRows);
        //emit tới cha danh sách đối tượng
        this.$emit("eMultiSelect", this.checkedRows);
      },
  
      /**-------------------------------------------------
       * Hàm xử lý khi click vào nút [Sửa] trên bảng.
       * @param {Object} obj - đối tượng thao tác
       * @param {Number} formMode - trạng thái của form
       * Author: quyetkaito (27/02/2022)
       -----------------------------------------------*/
      handleEdit(obj) {
        //emit tới cha nhận sự kiện update
        this.$emit("onUpdate", { data: obj, formMode: 2 });
        //emit tới cha đóng function
        this.$emit("handleHideFunc");
      },
  
      /**------------------------------------------
      *Xử lý khi 1 hàng được click
      *Author: quyetkaito (13/04/2022)
      --------------------------------------------*/
      handleClick(object, index) {
        try {
          //emit tới cha đối tượng được click
          this.$emit("eClick", { object, index });
          //xóa background cho tất cả các hàng khác, giữ lại hàng đang được click
          this.$refs.mTable.querySelectorAll(`tbody tr`).forEach((e) => {
            e.classList.remove("selected");
          });
          this.$refs.mTable
            .querySelector(`tbody tr:nth-child(${index + 1})`)
            .classList.add("selected");
        } catch (error) {
          //log ra lỗi
          console.log(error);
        }
      },
  
      /**------------------------------------------------
           * Hàm xử lý khi click vào nút [Xóa] trên bảng.
           * @param {Object} obj - đối tượng cần xóa.
           * Author: quyetkaito (27/02/2022)
           ----------------------------------------------*/
      handleDelete(obj) {
        this.$emit("onDelete", obj);
      },
  
      /**------------------------------------------------------
       * Hàm thực hiện lấy vị trí click để hiện dropdown content.
       * @param {Object} object - đối tượng ứng với hàng.
       * @param {Event} event
       * @param {Number} index - số thứ tự của hàng được chọn
       * Author: quyetkaito (07/03/2022)
       -----------------------------------------------------------*/
      showOptions(event, object, index) {
        // console.log(index);
        // console.log(this.$refs.dropdownArrow[index]);
        //lấy ra arrow tại hàng đó
        let arrow = this.$refs.dropdownArrow[index];
        //lấy ra vị trí của arrow
        let arrowPosition = arrow.getBoundingClientRect();
  
        //emit tới cha show dropdowncontent hiện tại
        this.$emit("eShowFunc", { object, arrowPosition });
      },
  
      /**------------------------------------------------
       * Xử lý đóng table Function khi click ra ngoài bảng
       * Author: quyetkaito (13/03/2022)
      -------------------------------------*/
      hideOptions() {
        //emit tới cha ẩn dropdowncontent hiện tại
        this.$emit("handleHideFunc");
      },
  
      /**------------------------------------------
      *CSS cho những hàng đang được chọn
      *@param {Object} selectedRow - hàng đang chọn.
      selectedRow, value
      *Author: quyetkaito (15/04/2022)
      --------------------------------------------*/
      addSelectedRow(index) {
        if (this.listData.length > 0) {
          let allRow = this.$el.querySelectorAll(`.trGroupVendor`);

          let thisRow = this.$el.querySelector(
            `.trGroupVendor:nth-child(${index + 1})`
          );
          
          if (allRow) {
            //xóa background cho tất cả các hàng khác, giữ lại hàng đang được click
            allRow.forEach((e) => {
              e.classList.remove("active");
            });
            if (thisRow) {
              thisRow.classList.add("active");
              // this.$el.querySelector("table.table thead tr").classList.remove("selected");
            }
          }
        }
      },
  
      /**------------------------------------------
      *Hàm lấy thông tin 1 hàng của table.
      *@param {Number} index số thứ tự hàng, bắt đầu từ 0
      *Author: quyetkaito (01/05/2022)
      --------------------------------------------*/
      getRow(index){
        return this.listData[index];
      }
    },
  
    /**------------------------------------------
    *Tính toán data trong grid.
    *Author: quyetkaito (09/04/2022)
    --------------------------------------------*/
    computed: {
      /**-------------------------------------
       * Thực hiện chọn tất cả/ bỏ chọn tất cả
       * Author: quyetkaito (14/03/2022)
       ---------------------------------*/
      selectAll: {
        get: function () {
          //trả về true nếu listData = số hàng được check, false ngược lại
          return this.listData
            ? this.checkedRows.length == this.listData.length &&
                this.listData.length != 0
            : false;
        },
  
        //select All
        set: function (value) {
          var selected = [];
          try {
            if (value) {
              console.log(value);
              //tạo danh sách các đối tượng được chọn
              this.listData.forEach(function (data) {
                selected.push(data);
              });
            }
            this.checkedRows = selected;
          } catch (error) {
            this.checkedRows = selected;
            console.log(error);
          }
        },
      },
  
      /**------------------------------
       * Tính toán số lượng các cột skeleton.
       * Author: quyetkaito (27/03/2022)
       --------------------------------*/
      columnSkeleton() {
        if (this.isCheckable) {
          return this.listHeader.length + 2;
        } else {
          return this.listHeader.length + 1;
        }
      },
    },
  };
  </script>
  
  <style>
  .tool-btn {
    white-space: nowrap;
    font-weight: 600 !important;
  }
  </style>
  