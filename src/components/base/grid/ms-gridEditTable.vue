<template>
    <div class="grid-container">
      <div class="grid-editable">
        <table
          ref="msTable"
          class="m-table"
          :class="{ 'border-bottom-only': isBorderBottom }"
        >
          <!-- Table Header -->
          <thead v-if="isHasHeader">
            <tr>
              <th v-if="isDragable" style="width: 50px"></th>
              <th v-if="isCount" style="width: 50px; max-width: 50px;position: sticky;top: 0;left: 0;min-width: 25px;text-align: center">#</th>
              <th v-if="isCheckable" class="w-50">
                <div class="m-checkbox">
                  <input type="checkbox" v-model="selectAll" ref="selectAll" />
                </div>
              </th>
              <!-- Lấy tên các header để render ra -->
              <th
                v-for="(header, index) in listHeader"
                :key="index"
                class="thSticky"
                :style="`width:${header.columnWidth}px!important;min-width:${header.columnWidth}px!important;`"
              >
                <div :title="header.textFull" :class="[header.align]">
                  {{ header.text }}
                </div>
              </th>
              <th v-if="isHasFunctionColumn"></th>
            </tr>
          </thead>
  
          <!-- Table Body -->
          <tbody v-if="listData.length !== 0">
            <!-- Vòng for tạo các hàng -->
            <tr
              v-for="(object, index) in listData"
              :key="index"
              @click="toggleCss(index)"
            >
              <td v-if="isDragable" style="width: 50px">
                <div class="flex align-center">
                  <div class="m-icon m-icon-16 m-drag-grid"></div>
                </div>
              </td>
  
              <td v-if="isCount" class="tdCount" style="min-width: 25px; max-width: 50px">
                <div class="align-center">
                  {{ index + 1 }}
                </div>
              </td>
  
              <!-- Cột Checkbox -->
              <td v-if="isCheckable" class="w-50">
                <div class="m-checkbox">
                  <input type="checkbox" v-model="checkedRows" :value="object" />
                </div>
              </td>
  
              <!-- Vòng for tạo các cột -->
              <td
                v-for="(column, idx) in listHeader"
                :key="idx"
                :style="`width:${column.columnWidth}px!important;min-width:${column.columnWidth}px!important;`"
              >
                <div
                  :class="[
                    column.align,
                    { 'align-x-center': column.type == 'checkbox' },
                    { 'justify-end': column.type == 'input-number' },
                    { 'stykyTD': column.type == 'textarea' },
                  ]"
                >
                  <!-- Nếu là input -->
                  <ms-input
                    v-if="column.type == 'input' && !disabled"
                    v-model="object[column.fieldName]"
                    :isHasLabel="false"
                   :styleClass="column.style"
                    style="width: 100%"
                    @focus="focus(idx)"
                  />
  
                  <!-- Nếu là input number -->
                  <ms-input
                    v-if="column.type == 'input-number' && !disabled"
                    :isNumberInput="true"
                    :isHasLabel="false"
                    :maxlength="18"
                    styleClass="paddingLeft"
                    v-model="object[column.fieldName]"
                    style="width: 100%"
                    @blur="onBlur(object[column.fieldName], column.fieldName)"
                  />
  
                  <!-- Nếu là text bình thường (ko sửa được, có thể là ngày tháng => gọi hàm format)-->
                  <div
                    v-if="
                      (column.type != 'input' &&
                        column.type != 'input-number' &&
                        column.type != 'selectbox' &&
                        column.type != 'checkbox' &&
                        column.type != 'textarea') ||
                      disabled
                    "
                  >
                    {{ object[column.fieldName] }}
                  </div>
                  <!-- Nếu là checkbox -->
                  <ms-checkbox
                    v-if="column.type == 'checkbox'"
                    v-model="object[column.fieldName]"
                    :isHasLabel="false"
                  />
  
                  <!-- Nếu là textarea -->
                  <ms-input
                    v-if="column.type == 'textarea'"
                    :isTextArea="true"
                    :isHasLabel="false"
                    placeholder=""
                    :height="32"
                    :style="{ width: '100%' }"
                  />
  
                  <!-- Nếu là selectbox-->
                  <ms-combobox
                    :ref="column.fieldName + index"
                    :idStyle="column.id"
                    v-if="column.type == 'selectbox' && !disabled"
                    :options="column.comboOption"
                    :showHeader="true"
                    :appendToBody="true"
                    :isPlus="false"
                    class="revMargin"
                    :label="column.comboLabel"
                    :value="column.comboValue"
                    :listHeader="column.comboHeader"
                    v-model="object[column.fieldName]"
                    :isHasLabel="false"
                    @onSelected="onSelected"
                    :disabled="disabled"
                  />
                </div>
              </td>
  
              <!-- Cột chức năng -->
              <td v-if="isHasFunctionColumn" class="w-50">
                <!-- Nút xóa -->
                <div class="text-align-center justify-center">
                  <div
                    class="m-icon m-icon-16 m-delete"
                    @click="removeRow(index)"
                  ></div>
                </div>
              </td>
            </tr>
          </tbody>
  
          <!-- Table Footer -->
          <tfoot v-if="showFooter" class="none-border">
            <tr>
              <th v-if="isDragable" class="w-50"></th>
              <th v-if="isCheckable" class="w-50"></th>
              <th v-if="isCount"></th>
              <th
                v-for="(header, index) in listHeader"
                :key="index"
                :style="`width:${header.columnWidth}px!important;min-width:${header.columnWidth}px!important;`"
              >
                <div v-if="header.type == 'input-number'" class="justify-end">
                  {{ header.total_amount }}
                </div>
              </th>
              <th v-if="isHasFunctionColumn"></th>
            </tr>
          </tfoot>
        </table>
      </div>
      <div v-if="isChangeRow" class="flex mt-10">
        <ms-button
          buttonType="m-btn-second btn-sm"
          text="Thêm dòng"
          class="mr-12"
          @click="addRow()"
        />
        <ms-button
          buttonType="m-btn-second btn-sm"
          text="Xóa hết dòng"
          @click="handleRemoveAllRow()"
        />
      </div>
    </div>
    <ms-message ref="messageBox" :text="messageText" :type="messageType" />
  </template>
  <script>

  
  //import myjs
  import { formatDate } from "@/js/base";
  import Enum from "@/js/enum";
  import { formatCurrency } from "@/js/base";
  import msInput from '@/components/base/input/ms-input.vue';
  import msCombobox from '@/components/base/combobox/ms-combobox.vue';
  
  export default {
    component: {},
    emits: [
      "eRemoveRow",
      "eUpdateRow",
      "eAddRow",
      "eRemoveAllRow",
      "eCheckRow",
      "eNumberInput",
      "eComboSelected",
    ],
    components: {msInput,msCombobox},
  
    /**------------------------------------------
    *Các prop của grid-editable
    *Author: quyetkaito (10/04/2022)
    --------------------------------------------*/
    props: {
      //list data render ra bảng
      listData: {
        type: Array, //một mảng các object data
        default: () => [],
        required: true, //bắt buộc có
      },
      //table có header hay không?
      isHasHeader: {
        type: Boolean,
        default: true,
      },
      //list các header của bảng
      listHeader: {
        type: Array,
        default: () => [
          {
            text: "Cột 1",
            fieldName: "FieldName1",
            align: "text-align-left",
            type: "text",
          },
          {
            text: "Cột 2",
            fieldName: "FieldName2",
            align: "text-align-left",
            width: "w-200",
            type: "input",
          },
          {
            text: "Cột 3",
            fieldName: "FieldName3",
            align: "text-align-left",
            width: "w-70",
            type: "input",
          },
        ],
      },
      //có cột checkbox hay không?
      isCheckable: {
        type: Boolean,
        default: true,
      },
      //có phải bảng setting không?
      isSetting: {
        type: Boolean,
        default: false,
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
      //có nút thêm dòng và xóa hết dòng hay khong?
      isChangeRow: {
        type: Boolean,
        default: true,
      },
      //bảng này có phải là chỉ border bottom thôi hay không?
      isBorderBottom: {
        type: Boolean,
        default: false,
      },
      //có show footer hay không?
      showFooter: {
        type: Boolean,
        default: false,
      },
      //có cột đếm số thứ tự không?
      isCount: {
        type: Boolean,
        default: false,
      },
      //bảng có bị diasbled hay không?
      disabled: {
        type: Boolean,
        default: false,
      },
    },
  
    //data của grid-editable
    data() {
      return {
        row: null, //một hàng là 1 đối tượng
        checkedRows: [], //danh sách hàng đang được chọn
        tangID : 0,
        //message
        messageText: "message-text",
        messageType: 0,
        comboboxOptions: [
          { label: "option 1", value: "1" },
          { label: "option 2", value: "2" },
          { label: "option 3", value: "3" },
        ],
        comboboxValue: null,
        comboboxHeader: [{ fieldName: "label" }],
        inputNumber: null,
        selectedRow: null, //hàng đang chọn
      };
    },
    created() {},
    mounted() {},
    watch: {
      /**----------------------------------------------------------
       * nếu checkedRows thay đổi thì cập nhật lại các hàng dc select
       * Author: quyetkaito (14/03/2022)
       ---------------------------------*/
      checkedRows() {
        this.changeSelected();
      },
  
      /**------------------------------------------
      *Nếu listData thay đổi thì set lại các cột đang được check.
      *Author: quyetkaito (11/04/2022)
      --------------------------------------------*/
      // listData() {
      //   console.log("list data thay doi");
      //   this.setCheckedList();
      // },
  
      listData: {
        deep: true,
        handler() {
          // console.log("list data thay doi");
          // console.log(this.listData);
          this.setCheckedList();
        },
      },
    },
  
    //method của grid-editable
    methods: {
      /**------------------------------------------
      *Nhận sự kiện chọn từ combobox.
      *Author: quyetkaito (20/04/2022)
      --------------------------------------------*/
      onSelected(object) {
        // console.log("object nhan dc: ", object);
        //emit lại cha object nhan dc
        this.$emit("eComboSelected", { object: object, row: this.selectedRow });
      },
  
      /**------------------------------------------
      *Xử lý ẩn hiện input, dropdown khi click vào hàng
      *@param {Number} index: vị trí hàng
      *Author: quyetkaito (20/04/2022)
      --------------------------------------------*/
      toggleCss(index) {
        if (this.listData.length > 0) {
          this.selectedRow = index;
          //xóa background cho tất cả các hàng khác, giữ lại hàng đang được click
          this.$refs.msTable
            .querySelectorAll(`.grid-editable>table>tbody>tr`)
            .forEach((e) => {
              e.classList.remove("nodisabled");
            });
          this.$refs.msTable
            .querySelector(
              `.grid-editable>table>tbody>tr:nth-child(${index + 1})`
            )
            .classList.add("nodisabled");
        }
      },
  
      /**------------------------------------------
      *Xử lý khi blur khỏi ô input số
      *@param {String} value: giá trị hiện tại của ô input
      *@param {String} fieldName: xác định nó là cột nào
      *Author: quyetkaito (17/04/2022)
      --------------------------------------------*/
      onBlur(value, fieldName) {
        //emit tới cha giá trị của ô input.
        this.$emit("eNumberInput", { value, fieldName });
      },
  
      /**------------------------------------------
      *Set các hàng đang được check trong bảng.
      *Author: quyetkaito (11/04/2022)
      --------------------------------------------*/
      setCheckedList() {
        try {
          //khởi tạo 1 list mới
          let checkedList = [];
  
          //duyệt từng phần tử trong mảng data
          this.listData.forEach((e) => {
            //nếu cột show có và = true thì push vào mảng
            if (e.Show) {
              checkedList.push(e);
            }
          });
  
          //gán danh sách các cột được check bằng mảng mới.
          this.checkedRows = checkedList;
        } catch (error) {
          //log ra lỗi
          console.log(error);
        }
      },
  
      /**--------------------------------------
       * Xử lý khi nút [Thêm dòng] được click.
       * Author: quyetkaito (01/04/2022)
      ----------------------------------------*/
      addRow() {
        //nếu bảng đang bị disabled thì ko cho thêm dòng mới
        if (this.disabled) return;
        //emit tới cha push một object rỗng vào
        this.$emit("eAddRow");
      },
  
      /**------------------------------------------
       * Hàm xử lý khi nút [xóa hết dòng] được click.
       * Author: quyetkaito (31/03/2022)
       -----------------------------------------*/
      handleRemoveAllRow() {
        //nếu bảng đang bị disabled
        if (this.disabled) return;
        //emit tới cha nhận sự kiện xóa.
        this.$emit("eRemoveAllRow");
      },
  
      /**--------------------------------------
       * Hàm xử lý khi nút xóa hàng được click. (xóa client)
       * @param {Number} index: hàng cần xóa.
       * Author: quyetkaito (31/03/2022)
      ----------------------------------------*/
      removeRow(index) {
        //nếu bảng đang bị disabled
        if (this.disabled) return;
  
        //emit tới cha xóa hàng vói vị trí index đi.
        this.$emit("eRemoveRow", index);
      },
  
      /**---------------------------------------------
       * Thực hiện bỏ dấu checkAll sau khi xóa nhiều.
       * Author: quyetkaito (18/03/2022)
       ---------------------------------*/
      unCheckAll() {
        if (this.$refs.selectAll.checked == true)
          this.$refs.selectAll.checked = false;
      },
  
      /**-----------------------------------
       * Hàm show data tương ứng với các cột của header
       * @param {String} data - data của cột
       * @param {String} key - tên cột
       * Author: quyetkaito (16/03/2022)
       ------------------------------------*/
      showData(data, key) {
        //nếu dữ liệu dạng số
        if (typeof data[key] == "number") {
          return formatCurrency(data[key].toString());
        }
  
        //nếu là cột ngày thì cần format trước khi hiển thị lên bảng
        if (key === "date_of_birth" || key === "identity_date") {
          return formatDate(data[key], Enum.DateFormat);
        }
        //trả về data với field tương ứng từ header
        return data[key];
      },
  
      /**----------------------------------------------
       * Hàm emit tới cha danh sách các hàng được chọn.
       * Author: quyetkaito (15/03/2022)
      ------------------------------------*/
      changeSelected() {
        //emit tới cha danh sách các hàng được chọn.
        // console.log("list Checked:", this.checkedRows);
        this.$emit("eCheckRow", this.checkedRows);
      },
  
  
      /**------------------------------------------
      *Xử lý focus vào ô đầu khi thêm mới một hàng.
      *@param {Number} rowIndex - xác định là hàng nào.
      *Author: quyetkaito (22/04/2022)
      --------------------------------------------*/
      focus(rowIndex) {
        try {
          if (
            this.$refs.msTable.querySelectorAll(
              `.grid-editable>table>tbody>tr:nth-child(${rowIndex}) input`
            )
          ) {
            //tìm hàng, tìm input đầu tiên trong hàng đó và focus.
            this.$refs.msTable
              .querySelectorAll(
                `.grid-editable>table>tbody>tr:nth-child(${rowIndex}) input`
              )[0]
              .focus();
            //thay đổi css thành hàng được chọn.
            this.toggleCss(rowIndex - 1);
          }
        } catch (error) {
          //log ra lỗi
          console.log(error);
        }
      },
  
      /**------------------------------------------
      *Hàm thêm css cho ô bị lỗi
      *@param {String} refName - ref của ô cần add css
      *Author: quyetkaito (25/04/2022)
      --------------------------------------------*/
      addError(refName) {
        try {
          //thêm css error cho ô bị lỗi.
          this.toggleCss(0);
          this.$refs[refName][0].addError();        
        } catch (error) {
          //log ra lỗi
          console.log(error);
        }
      },
  
      /**------------------------------------------
      *Focus vào ô trong bảng.
      *@param {String} refName - ref của ô cần focus
      *Author: quyetkaito (25/04/2022)
      --------------------------------------------*/
      focusError(refName) {
        this.$refs[refName][0].focus();
      },
    },
  
    /**------------------------------------------
    *Tính toán data grid.
    *Author: quyetkaito (10/04/2022)
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
    },
  };
  </script>
  
  <style>
  .tool-btn {
    white-space: nowrap;
    font-weight: 600 !important;
  }
  .grid-container {
    display: flex;
    /* flex-direction: column; */
    height: 100%;
  }
  .grid-editable {
    overflow: auto;
  }
  .grid-editable .m-table{
    border-collapse: separate;
    border-spacing: unset;
  }
  .grid-editable .m-table tbody tr input {
    border: none;
  }
  .grid-editable .m-table tbody tr input[type="checkbox"] {
    border: 1px solid #babec5 !important;
  }
  
  .grid-editable .m-table tbody tr .dropdown-merge {
    display: none;
  }
 
  
  .grid-editable .m-table tbody tr.nodisabled {
    background-color: #e5f3ff;
  }
  
  .grid-editable .m-table tbody tr.nodisabled input {
     border: 1px solid #babec5 !important;
  }
  .grid-editable .m-table tbody tr.nodisabled input.error {
    border: 1px solid red !important;
  }
  .grid-editable .m-table tbody tr.nodisabled input.error:focus {
    border: 1px solid red !important;
  }
  .grid-editable .m-table tbody tr.nodisabled input:focus {
    border: 1px solid #2ca01c !important;
  }
  
  .grid-editable .m-table tbody tr.nodisabled .dropdown-merge {
    display: flex !important;
  }
  .w47{
    width: 47px !important;
  }
  .grid-editable .m-table tbody tr textarea {
    border: none;
}
.grid-editable .m-table tbody tr.nodisabled textarea {
    border: 1px solid #babec5 !important;
}
.tdCount{
  position:sticky;
  left: 0;
  top:0;
  text-align: center;
  background: white;
}
.grid-editable .m-table thead tr .nodisabled .thSticky{
  position: sticky;
  left: 45.5px;
  top:0;
}
.stykyTD{
  position:sticky;
  top:32.5px;
  left: 45.5px;
}
.grid-editable .m-table tbody tr td{
  border-right: 1px solid #c7c7c7;
    border-bottom: 1px solid #c7c7c7;
}
.grid-editable .m-table thead tr th{
  border-right: 1px solid #c7c7c7;
    border-bottom: 1px solid #c7c7c7;
}
  </style>
  