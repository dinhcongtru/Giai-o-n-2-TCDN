import { createApp } from 'vue'
import vue from 'vue';
import App from './App.vue'
import mitt from "mitt";
import VueClickAway from "vue3-click-away";
import {createRouter, createWebHistory} from 'vue-router';
import CashVue from "./views/Cash/CashVue.vue";
import ReceiptPaymentList from "./views/Receipt/ReceiptPaymentList.vue";
import CashProcess from "./views/Cash/CashProcess.vue"
import TheMain from "./components/TheMain.vue"
import TheVendor from "./views/Vendor/DIVendor.vue"
import ReceiptDetails from './views/Receipt/ReceiptDetalis.vue';
import 'devextreme/dist/css/dx.light.css';
//vue next select
// import VueSelect from 'vue-next-select'
// import 'vue-next-select/dist/index.min.css'
//vue select
// import vSelect from 'vue-select';
// import 'vue-select/dist/vue-select.css';

const emitter = mitt();
const app = createApp(App);
const routers = [
    {
        path: "/app/CA",
        name: "CashVue",
        component: CashVue,
        children: [{
            path: "CAProcess",
            name: "CashProcess",
            component: CashProcess
        },
        {
            path: "CAReceipt/CAReceiptPaymentList",
            name: "ReceiptPaymentList",
            component: ReceiptPaymentList
        },
     
    ]
    },
    {
        path: "/app/DI/DIEmployee",
        name: "TheMain",
        component: TheMain
    },
    {
        path: "/app/DI/DIVendor",
        name: "TheVendor",
        component: TheVendor
    },
    {
        path: "/app/DI/ReceiptDetails",
        name: "ReceiptDetail",
        component: ReceiptDetails
    },
    
   
]
const router = createRouter({
    history: createWebHistory(),
    routes: routers
})
app.use(router)
app.use(VueClickAway);
app.use(vue);
app.config.globalProperties.emitter = emitter;
app.mount("#app");

app.directive("clickoutside", {
    inserted: (el, binding, vnode) => {
        // assign event to the element
        el.clickOutsideEvent = function (event) {
            // here we check if the click event is outside the element and it's children
            if (!(el == event.target || el.contains(event.target))) {
                // if clicked outside, call the provided method
                vnode.context[binding.expression](event);
            }
        };
        // register click and touch events
        document.body.addEventListener("click", el.clickOutsideEvent);
        document.body.addEventListener("touchstart", el.clickOutsideEvent);
    },
    unbind: function (el) {
        // unregister click and touch events before the element is unmounted
        document.body.removeEventListener("click", el.clickOutsideEvent);
        document.body.removeEventListener("touchstart", el.clickOutsideEvent);
    },
    stopProp(event) {
        event.stopPropagation();
    },
});
