<template lang="">
    <div class="table__row">
      <MsTableCellVue
        v-for="itemHeader in tableHeader"
        :key="itemHeader.id"
        :className="itemHeader.typeDisplay == 'checkbox' ? 'text_center' : ''"
      >
        <MsCheckbox
          :readOnly="true"
          :active="item[itemHeader.label]"
          v-if="itemHeader.typeDisplay == 'checkbox'"
        ></MsCheckbox>
        <span v-else>{{ item[itemHeader.label] }}</span></MsTableCellVue
      >
    </div>
  </template>
  <script>
  import MsTableCellVue from "./MsTableCell.vue";
  import { tableHeader, fakeData } from "../../../resource/resourceVN";
  import MsCheckbox from "../checkbox/MsCheckbox.vue";
  export default {
    data() {
      return {
        tableHeader,
        fakeData,
      };
    },
    components: {
      MsTableCellVue,
      MsCheckbox,
    },
    props: {
      item: {
        type: Object,
        default: () => {},
      },
    },
  };
  </script>
  <style lang="css" scoped>
  .table__row {
    width: 100%;
    display: flex;
    flex-wrap: nowrap;
    font-size: 12px;
  }
  .table__row > div {
    width: 250px;
    flex-shrink: 0;
    padding: 0 16px;
    height: 28px;
    display: flex;
    align-items: center;
    /* overflow: hidden; */
    text-overflow: ellipsis;
    white-space: nowrap;
    border-bottom: 1px solid var(--border-color);
  }
  
  .table__row > div + div {
    border-left: 1px solid var(--border-color);
  }
  </style>
  