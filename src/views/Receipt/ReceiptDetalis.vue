<template>
    <div class="base-view-detail">
        <div class="header-layout">
            <div class="top-header">
                <div class="recent-log-btn"></div>
                <div class="title-payment">Phiếu chi pc161</div>
                <div class="header-detail-input">
                    <the-dropdown
                        type="text"
                        title="Đơn vị không được để trống."                   
                        id="width334"
                        tabindex="3"
                        idStyle="id9"
                        :departmentList="departmentList"
                        :departmentId="employee.departmentID"
                        :departmentName="employee.departmentName"
                        :departmentError="error.departmentName"
                        @departmentId="getDepartmentId($event)"
                        @departmentName="getDepartmentName($event)"
                    />
                </div>
                <div class="header-detail-buttons">
                    <div class="guide-tour">
                        <div class="mi-tour"></div>
                        <div class="tour-laybel">Hướng dẫn</div>
                    </div>
                    <div class="con-ms-tooltip" title="Tùy chỉnh giao diện">
                        <div class="mi-setting-detail"></div>
                    </div>
                    <div class="con-ms-tooltip" title="Giúp(F1)">
                        <div class="mi-help"></div>
                    </div>
                    <div class="con-ms-tooltip" @click="onCancelModal" title="Đóng(ESC)">
                        <div class="mi-close"></div>
                    </div>
                </div>
            </div>
        </div>
        <div class="body-layout">
            <div class="layout-top">
                <div class="w-4/5 right-separate">
                    <div class="merge">
                        <div class="obj-one">
                            <div class="obj-ma">
                                <div class="obj-ma-title">Mã đối tượng</div>
                                <the-dropdown
                                type="text"
                                title="Đơn vị không được để trống."                   
                                idStyle="id8"
                                tabindex="3"
                                :departmentList="departmentList"
                                :departmentId="employee.departmentID"
                                :departmentName="employee.departmentName"
                                :departmentError="error.departmentName"
                                @departmentId="getDepartmentId($event)"
                                @departmentName="getDepartmentName($event)"
                            />
                            </div>
                            <div class="obj-name">
                                <div class="obj-ma-title">Tên đối tượng</div>
                                <input type="text" class="input-objName">
                            </div>
                        </div>
                        <div class="obj-two">
                            <div class="obj-ma">
                                <div class="obj-ma-title">Người nhận</div>
                                <input type="text" class="input-nhan">
                            </div>
                            <div class="obj-name">
                                <div class="obj-ma-title">Địa chỉ</div>
                                <input type="text" class="input-objName">
                            </div>
                        </div>
                        <div class="obj-three">
                            <div class="regen">
                                <div class="obj-ma-title">Lý do chi</div>
                                <input type="text" name="" id="" class="w-full" value="Chi tiền cho">
                            </div>
                        </div>
                    </div>
                    <div class="obj-four">
                        <div class="obj-ma">
                            <div class="obj-ma-title">Nhân viên</div>
                            <the-dropdown
                            type="text"
                            title="Đơn vị không được để trống."                   
                            idStyle="id7"
                            tabindex="3"
                            :departmentList="departmentList"
                            :departmentId="employee.departmentID"
                            :departmentName="employee.departmentName"
                            :departmentError="error.departmentName"
                            @departmentId="getDepartmentId($event)"
                            @departmentName="getDepartmentName($event)"
                        />
                        </div>
                        <div class="kem-theo">
                            <div class="obj-ma-title">Kèm theo</div>
                            <div class="merge-row">
                                <input type="text" name="" id="" class="input-number" placeholder="Số lượng">
                                <div class="title-gim">chứng từ gốc</div>
                            </div>
                        </div>
                    </div>
                    <div class="obj-five">
                        <div class="row-tham-chieu">Tham chiếu</div>
                    </div>

                </div>
                <div class="w-1/5 width20">
                    <div class="m-row left-separate">
                        <div class="obj-ma-title">Ngày hạch toán</div>
                        <input type="date" class="datepacker" value="2020-12-20">
                    </div>
                    <div class="m-row left-separate">
                        <div class="obj-ma-title">Ngày phiếu chi</div>
                        <input type="date" class="datepacker"  value="2020-12-20">
                    </div>
                    <div class="m-row left-separate">
                        <div class="obj-ma-title">Số phiếu chi</div>
                        <input type="text" class="datepacker sochi" value="pc161">
                    </div>
                </div>
                <div class="w-1/4 summary-info">
                    <div class="summary-info-title">Tổng tiền</div>
                    <h1 class="summary-info-number">0,0</h1>
                </div>
            </div>
            <div class="layout-bottom">
                <div class="header-bottom">
                    <div class="title-bottom">Hạch toán</div>
                    <div class="merge-bottom">
                        <div class="title-type-money">Loại tiền</div>
                        <div class="type-money">
                            <the-dropdown
                                type="text"
                                title="Đơn vị không được để trống."                   
                                id="w100"
                                idStyle="id6"
                                tabindex="3"
                                :departmentList="departmentList"
                                :departmentId="employee.departmentID"
                                :departmentName="employee.departmentName"
                                :departmentError="error.departmentName"
                                @departmentId="getDepartmentId($event)"
                                @departmentName="getDepartmentName($event)"
                            />
                        </div>
                    </div>
                </div>
                <div class="layout-bot-main">
                    <div class="main-layout-table">
                        <table class="table">
                            <thead>
                                <tr>
                                    <th align="center" class="thead1" style="min-width:20px">
                                        #
                                    </th>
                                    <th align="left" class="thead2" style="min-width:120px">DIỄN GIẢI</th>
                                    <th align="left" style="min-width:180px">TK NỢ</th>
                                    <th align="left" style="min-width:60px">TK CÓ</th>
                                    <th align="right" style="min-width:100px">SỐ TIỀN</th>
                                    <th align="left" style="min-width:115px">ĐỐI TƯỢNG</th>
                                    <th align="left" style="min-width:134px">TÊN ĐỐI TƯỢNG</th>
                                    <th align="left" style="min-width:120px">TK NGÂN HÀNG</th>
                                    <th align="left" style="min-width:156px">KHẾ ƯỚC VAY</th>
                                    <th align="left" style="min-width:235px;">KHOẢN MỤC CP</th>
                                    <th align="left" style="min-width:235px;">TÊN KHOẢN MỤC CP</th>
                                    <th align="left" style="min-width:235px;">ĐƠN VỊ</th>
                                    <th align="left" style="min-width:235px;">TÊN ĐƠN VỊ</th>
                                    <th align="left" style="min-width:235px;">ĐỐI TƯỢNG THCP</th>
                                    <th align="left" style="min-width:235px;">TÊN ĐỐI TƯỢNG THCP</th>
                                    <th align="left" style="min-width:235px;">CÔNG TRÌNH</th>
                                    <th align="left" style="min-width:235px;">TÊN CÔNG TRÌNH</th>
                                    <th align="left" style="min-width:235px;">ĐƠN ĐẶT HÀNG</th>
                                    <th align="left" style="min-width:235px;">HỢP ĐỒNG BÁN</th>
                                    <th align="left" style="min-width:235px;">ĐƠN MUA HÀNG</th>
                                    <th align="left" style="min-width:235px;">HỢP ĐỒNG MUA</th>
                                    <th align="left" style="min-width:235px;">MỤC THU/CHI</th>
                                    <th align="left" style="min-width:235px;">TÊN MỤC THU/CHI</th>
                                    <th align="left" style="min-width:235px;">MÃ THỐNG KÊ</th>
                                    <th align="left" style="min-width:235px;">TÊN MÃ THỐNG KÊ</th>
                                    <th align="left" style="min-width:235px;">CP KHÔNG HỢP LÝ</th>
                                    <th align="left" style="min-width:235px;">TRƯỜNG MỞ RỘNG 1</th>
                                    <th align="left" style="min-width:235px;">TRƯỜNG MỞ RỘNG 2</th>
                                    <th align="left" style="min-width:235px;">TRƯỜNG MỞ RỘNG 3</th>
                                    <th align="left" style="min-width:235px;">TRƯỜNG MỞ RỘNG 4</th>
                                    <th align="left" style="min-width:235px;">TRƯỜNG MỞ RỘNG 5</th>
                                    <th class="item-cn">
                                       
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>   
                                    <td align="center" class="thead1" style="min-width:20px">
                                        1
                                    </td>
                                    <td align="left" class="thead2" style="min-width:120px">DIỄN GIẢI</td>
                                    <td align="left" style="min-width:180px">TK NỢ</td>
                                    <td align="left" style="min-width:60px">TK CÓ</td>
                                    <td align="right" style="min-width:100px">SỐ TIỀN</td>
                                    <td align="left" style="min-width:115px">ĐỐI TƯỢNG</td>
                                    <td align="left" style="min-width:134px">TÊN ĐỐI TƯỢNG</td>
                                    <td align="left" style="min-width:120px">TK NGÂN HÀNG</td>
                                    <td align="left" style="min-width:156px">KHẾ ƯỚC VAY</td>
                                    <td align="left" style="min-width:235px;">KHOẢN MỤC CP</td>
                                    <td align="left" style="min-width:235px;">TÊN KHOẢN MỤC CP</td>
                                    <td align="left" style="min-width:235px;">ĐƠN VỊ</td>
                                    <td align="left" style="min-width:235px;">TÊN ĐƠN VỊ</td>
                                    <td align="left" style="min-width:235px;">ĐỐI TƯỢNG THCP</td>
                                    <td align="left" style="min-width:235px;">TÊN ĐỐI TƯỢNG THCP</td>
                                    <td align="left" style="min-width:235px;">CÔNG TRÌNH</td>
                                    <td align="left" style="min-width:235px;">TÊN CÔNG TRÌNH</td>
                                    <td align="left" style="min-width:235px;">ĐƠN ĐẶT HÀNG</td>
                                    <td align="left" style="min-width:235px;">HỢP ĐỒNG BÁN</td>
                                    <td align="left" style="min-width:235px;">ĐƠN MUA HÀNG</td>
                                    <td align="left" style="min-width:235px;">HỢP ĐỒNG MUA</td>
                                    <td align="left" style="min-width:235px;">MỤC THU/CHI</td>
                                    <td align="left" style="min-width:235px;">TÊN MỤC THU/CHI</td>
                                    <td align="left" style="min-width:235px;">MÃ THỐNG KÊ</td>
                                    <td align="left" style="min-width:235px;">TÊN MÃ THỐNG KÊ</td>
                                    <td align="left" style="min-width:235px;">CP KHÔNG HỢP LÝ</td>
                                    <td align="left" style="min-width:235px;">TRƯỜNG MỞ RỘNG 1</td>
                                    <td align="left" style="min-width:235px;">TRƯỜNG MỞ RỘNG 2</td>
                                    <td align="left" style="min-width:235px;">TRƯỜNG MỞ RỘNG 3</td>
                                    <td align="left" style="min-width:235px;">TRƯỜNG MỞ RỘNG 4</td>
                                    <td align="left" style="min-width:235px;">TRƯỜNG MỞ RỘNG 5</td>
                                    <td class="item-cn">
                                        <div class="cn-edit">
                                           
                                           <span class="mi-delete-table" >
                                           
                                           </span>
                                       </div>
                                    </td>                    
                                </tr>
                                <tr>   
                                    <td align="center" class="thead1" style="min-width:20px">
                                        1
                                    </td>
                                    <td align="left" class="thead2" style="min-width:120px">DIỄN GIẢI</td>
                                    <td align="left" style="min-width:180px">TK NỢ</td>
                                    <td align="left" style="min-width:60px">TK CÓ</td>
                                    <td align="right" style="min-width:100px">SỐ TIỀN</td>
                                    <td align="left" style="min-width:115px">ĐỐI TƯỢNG</td>
                                    <td align="left" style="min-width:134px">TÊN ĐỐI TƯỢNG</td>
                                    <td align="left" style="min-width:120px">TK NGÂN HÀNG</td>
                                    <td align="left" style="min-width:156px">KHẾ ƯỚC VAY</td>
                                    <td align="left" style="min-width:235px;">KHOẢN MỤC CP</td>
                                    <td align="left" style="min-width:235px;">TÊN KHOẢN MỤC CP</td>
                                    <td align="left" style="min-width:235px;">ĐƠN VỊ</td>
                                    <td align="left" style="min-width:235px;">TÊN ĐƠN VỊ</td>
                                    <td align="left" style="min-width:235px;">ĐỐI TƯỢNG THCP</td>
                                    <td align="left" style="min-width:235px;">TÊN ĐỐI TƯỢNG THCP</td>
                                    <td align="left" style="min-width:235px;">CÔNG TRÌNH</td>
                                    <td align="left" style="min-width:235px;">TÊN CÔNG TRÌNH</td>
                                    <td align="left" style="min-width:235px;">ĐƠN ĐẶT HÀNG</td>
                                    <td align="left" style="min-width:235px;">HỢP ĐỒNG BÁN</td>
                                    <td align="left" style="min-width:235px;">ĐƠN MUA HÀNG</td>
                                    <td align="left" style="min-width:235px;">HỢP ĐỒNG MUA</td>
                                    <td align="left" style="min-width:235px;">MỤC THU/CHI</td>
                                    <td align="left" style="min-width:235px;">TÊN MỤC THU/CHI</td>
                                    <td align="left" style="min-width:235px;">MÃ THỐNG KÊ</td>
                                    <td align="left" style="min-width:235px;">TÊN MÃ THỐNG KÊ</td>
                                    <td align="left" style="min-width:235px;">CP KHÔNG HỢP LÝ</td>
                                    <td align="left" style="min-width:235px;">TRƯỜNG MỞ RỘNG 1</td>
                                    <td align="left" style="min-width:235px;">TRƯỜNG MỞ RỘNG 2</td>
                                    <td align="left" style="min-width:235px;">TRƯỜNG MỞ RỘNG 3</td>
                                    <td align="left" style="min-width:235px;">TRƯỜNG MỞ RỘNG 4</td>
                                    <td align="left" style="min-width:235px;">TRƯỜNG MỞ RỘNG 5</td>
                                    <td class="item-cn">
                                        <div class="cn-edit">
                                           
                                           <span class="mi-delete-table" >
                                           
                                           </span>
                                       </div>
                                    </td>                    
                                </tr>
                                <tr>   
                                    <td align="center" class="thead1" style="min-width:20px">
                                        1
                                    </td>
                                    <td align="left" class="thead2" style="min-width:120px">DIỄN GIẢI</td>
                                    <td align="left" style="min-width:180px">TK NỢ</td>
                                    <td align="left" style="min-width:60px">TK CÓ</td>
                                    <td align="right" style="min-width:100px">SỐ TIỀN</td>
                                    <td align="left" style="min-width:115px">ĐỐI TƯỢNG</td>
                                    <td align="left" style="min-width:134px">TÊN ĐỐI TƯỢNG</td>
                                    <td align="left" style="min-width:120px">TK NGÂN HÀNG</td>
                                    <td align="left" style="min-width:156px">KHẾ ƯỚC VAY</td>
                                    <td align="left" style="min-width:235px;">KHOẢN MỤC CP</td>
                                    <td align="left" style="min-width:235px;">TÊN KHOẢN MỤC CP</td>
                                    <td align="left" style="min-width:235px;">ĐƠN VỊ</td>
                                    <td align="left" style="min-width:235px;">TÊN ĐƠN VỊ</td>
                                    <td align="left" style="min-width:235px;">ĐỐI TƯỢNG THCP</td>
                                    <td align="left" style="min-width:235px;">TÊN ĐỐI TƯỢNG THCP</td>
                                    <td align="left" style="min-width:235px;">CÔNG TRÌNH</td>
                                    <td align="left" style="min-width:235px;">TÊN CÔNG TRÌNH</td>
                                    <td align="left" style="min-width:235px;">ĐƠN ĐẶT HÀNG</td>
                                    <td align="left" style="min-width:235px;">HỢP ĐỒNG BÁN</td>
                                    <td align="left" style="min-width:235px;">ĐƠN MUA HÀNG</td>
                                    <td align="left" style="min-width:235px;">HỢP ĐỒNG MUA</td>
                                    <td align="left" style="min-width:235px;">MỤC THU/CHI</td>
                                    <td align="left" style="min-width:235px;">TÊN MỤC THU/CHI</td>
                                    <td align="left" style="min-width:235px;">MÃ THỐNG KÊ</td>
                                    <td align="left" style="min-width:235px;">TÊN MÃ THỐNG KÊ</td>
                                    <td align="left" style="min-width:235px;">CP KHÔNG HỢP LÝ</td>
                                    <td align="left" style="min-width:235px;">TRƯỜNG MỞ RỘNG 1</td>
                                    <td align="left" style="min-width:235px;">TRƯỜNG MỞ RỘNG 2</td>
                                    <td align="left" style="min-width:235px;">TRƯỜNG MỞ RỘNG 3</td>
                                    <td align="left" style="min-width:235px;">TRƯỜNG MỞ RỘNG 4</td>
                                    <td align="left" style="min-width:235px;">TRƯỜNG MỞ RỘNG 5</td>
                                    <td class="item-cn">
                                        <div class="cn-edit">
                                           
                                           <span class="mi-delete-table" >
                                           
                                           </span>
                                       </div>
                                    </td>                    
                                </tr>
                                <tr>   
                                    <td align="center" class="thead1" style="min-width:20px">
                                        1
                                    </td>
                                    <td align="left" class="thead2" style="min-width:120px">DIỄN GIẢI</td>
                                    <td align="left" style="min-width:180px">TK NỢ</td>
                                    <td align="left" style="min-width:60px">TK CÓ</td>
                                    <td align="right" style="min-width:100px">SỐ TIỀN</td>
                                    <td align="left" style="min-width:115px">ĐỐI TƯỢNG</td>
                                    <td align="left" style="min-width:134px">TÊN ĐỐI TƯỢNG</td>
                                    <td align="left" style="min-width:120px">TK NGÂN HÀNG</td>
                                    <td align="left" style="min-width:156px">KHẾ ƯỚC VAY</td>
                                    <td align="left" style="min-width:235px;">KHOẢN MỤC CP</td>
                                    <td align="left" style="min-width:235px;">TÊN KHOẢN MỤC CP</td>
                                    <td align="left" style="min-width:235px;">ĐƠN VỊ</td>
                                    <td align="left" style="min-width:235px;">TÊN ĐƠN VỊ</td>
                                    <td align="left" style="min-width:235px;">ĐỐI TƯỢNG THCP</td>
                                    <td align="left" style="min-width:235px;">TÊN ĐỐI TƯỢNG THCP</td>
                                    <td align="left" style="min-width:235px;">CÔNG TRÌNH</td>
                                    <td align="left" style="min-width:235px;">TÊN CÔNG TRÌNH</td>
                                    <td align="left" style="min-width:235px;">ĐƠN ĐẶT HÀNG</td>
                                    <td align="left" style="min-width:235px;">HỢP ĐỒNG BÁN</td>
                                    <td align="left" style="min-width:235px;">ĐƠN MUA HÀNG</td>
                                    <td align="left" style="min-width:235px;">HỢP ĐỒNG MUA</td>
                                    <td align="left" style="min-width:235px;">MỤC THU/CHI</td>
                                    <td align="left" style="min-width:235px;">TÊN MỤC THU/CHI</td>
                                    <td align="left" style="min-width:235px;">MÃ THỐNG KÊ</td>
                                    <td align="left" style="min-width:235px;">TÊN MÃ THỐNG KÊ</td>
                                    <td align="left" style="min-width:235px;">CP KHÔNG HỢP LÝ</td>
                                    <td align="left" style="min-width:235px;">TRƯỜNG MỞ RỘNG 1</td>
                                    <td align="left" style="min-width:235px;">TRƯỜNG MỞ RỘNG 2</td>
                                    <td align="left" style="min-width:235px;">TRƯỜNG MỞ RỘNG 3</td>
                                    <td align="left" style="min-width:235px;">TRƯỜNG MỞ RỘNG 4</td>
                                    <td align="left" style="min-width:235px;">TRƯỜNG MỞ RỘNG 5</td>
                                    <td class="item-cn">
                                        <div class="cn-edit">
                                           
                                           <span class="mi-delete-table" >
                                           
                                           </span>
                                       </div>
                                    </td>                    
                                </tr>
                                <tr>   
                                    <td align="center" class="thead1" style="min-width:20px">
                                        1
                                    </td>
                                    <td align="left" class="thead2" style="min-width:120px">DIỄN GIẢI</td>
                                    <td align="left" style="min-width:180px">TK NỢ</td>
                                    <td align="left" style="min-width:60px">TK CÓ</td>
                                    <td align="right" style="min-width:100px">SỐ TIỀN</td>
                                    <td align="left" style="min-width:115px">ĐỐI TƯỢNG</td>
                                    <td align="left" style="min-width:134px">TÊN ĐỐI TƯỢNG</td>
                                    <td align="left" style="min-width:120px">TK NGÂN HÀNG</td>
                                    <td align="left" style="min-width:156px">KHẾ ƯỚC VAY</td>
                                    <td align="left" style="min-width:235px;">KHOẢN MỤC CP</td>
                                    <td align="left" style="min-width:235px;">TÊN KHOẢN MỤC CP</td>
                                    <td align="left" style="min-width:235px;">ĐƠN VỊ</td>
                                    <td align="left" style="min-width:235px;">TÊN ĐƠN VỊ</td>
                                    <td align="left" style="min-width:235px;">ĐỐI TƯỢNG THCP</td>
                                    <td align="left" style="min-width:235px;">TÊN ĐỐI TƯỢNG THCP</td>
                                    <td align="left" style="min-width:235px;">CÔNG TRÌNH</td>
                                    <td align="left" style="min-width:235px;">TÊN CÔNG TRÌNH</td>
                                    <td align="left" style="min-width:235px;">ĐƠN ĐẶT HÀNG</td>
                                    <td align="left" style="min-width:235px;">HỢP ĐỒNG BÁN</td>
                                    <td align="left" style="min-width:235px;">ĐƠN MUA HÀNG</td>
                                    <td align="left" style="min-width:235px;">HỢP ĐỒNG MUA</td>
                                    <td align="left" style="min-width:235px;">MỤC THU/CHI</td>
                                    <td align="left" style="min-width:235px;">TÊN MỤC THU/CHI</td>
                                    <td align="left" style="min-width:235px;">MÃ THỐNG KÊ</td>
                                    <td align="left" style="min-width:235px;">TÊN MÃ THỐNG KÊ</td>
                                    <td align="left" style="min-width:235px;">CP KHÔNG HỢP LÝ</td>
                                    <td align="left" style="min-width:235px;">TRƯỜNG MỞ RỘNG 1</td>
                                    <td align="left" style="min-width:235px;">TRƯỜNG MỞ RỘNG 2</td>
                                    <td align="left" style="min-width:235px;">TRƯỜNG MỞ RỘNG 3</td>
                                    <td align="left" style="min-width:235px;">TRƯỜNG MỞ RỘNG 4</td>
                                    <td align="left" style="min-width:235px;">TRƯỜNG MỞ RỘNG 5</td>
                                    <td class="item-cn">
                                        <div class="cn-edit">
                                           
                                           <span class="mi-delete-table" >
                                           
                                           </span>
                                       </div>
                                    </td>                    
                                </tr>
                                <tr>   
                                    <td align="center" class="thead1" style="min-width:20px">
                                        1
                                    </td>
                                    <td align="left" class="thead2" style="min-width:120px">DIỄN GIẢI</td>
                                    <td align="left" style="min-width:180px">TK NỢ</td>
                                    <td align="left" style="min-width:60px">TK CÓ</td>
                                    <td align="right" style="min-width:100px">SỐ TIỀN</td>
                                    <td align="left" style="min-width:115px">ĐỐI TƯỢNG</td>
                                    <td align="left" style="min-width:134px">TÊN ĐỐI TƯỢNG</td>
                                    <td align="left" style="min-width:120px">TK NGÂN HÀNG</td>
                                    <td align="left" style="min-width:156px">KHẾ ƯỚC VAY</td>
                                    <td align="left" style="min-width:235px;">KHOẢN MỤC CP</td>
                                    <td align="left" style="min-width:235px;">TÊN KHOẢN MỤC CP</td>
                                    <td align="left" style="min-width:235px;">ĐƠN VỊ</td>
                                    <td align="left" style="min-width:235px;">TÊN ĐƠN VỊ</td>
                                    <td align="left" style="min-width:235px;">ĐỐI TƯỢNG THCP</td>
                                    <td align="left" style="min-width:235px;">TÊN ĐỐI TƯỢNG THCP</td>
                                    <td align="left" style="min-width:235px;">CÔNG TRÌNH</td>
                                    <td align="left" style="min-width:235px;">TÊN CÔNG TRÌNH</td>
                                    <td align="left" style="min-width:235px;">ĐƠN ĐẶT HÀNG</td>
                                    <td align="left" style="min-width:235px;">HỢP ĐỒNG BÁN</td>
                                    <td align="left" style="min-width:235px;">ĐƠN MUA HÀNG</td>
                                    <td align="left" style="min-width:235px;">HỢP ĐỒNG MUA</td>
                                    <td align="left" style="min-width:235px;">MỤC THU/CHI</td>
                                    <td align="left" style="min-width:235px;">TÊN MỤC THU/CHI</td>
                                    <td align="left" style="min-width:235px;">MÃ THỐNG KÊ</td>
                                    <td align="left" style="min-width:235px;">TÊN MÃ THỐNG KÊ</td>
                                    <td align="left" style="min-width:235px;">CP KHÔNG HỢP LÝ</td>
                                    <td align="left" style="min-width:235px;">TRƯỜNG MỞ RỘNG 1</td>
                                    <td align="left" style="min-width:235px;">TRƯỜNG MỞ RỘNG 2</td>
                                    <td align="left" style="min-width:235px;">TRƯỜNG MỞ RỘNG 3</td>
                                    <td align="left" style="min-width:235px;">TRƯỜNG MỞ RỘNG 4</td>
                                    <td align="left" style="min-width:235px;">TRƯỜNG MỞ RỘNG 5</td>
                                    <td class="item-cn">
                                        <div class="cn-edit">
                                           
                                           <span class="mi-delete-table" >
                                           
                                           </span>
                                       </div>
                                    </td>                    
                                </tr>
                                <tr>   
                                    <td align="center" class="thead1" style="min-width:20px">
                                        1
                                    </td>
                                    <td align="left" class="thead2" style="min-width:120px">DIỄN GIẢI</td>
                                    <td align="left" style="min-width:180px">TK NỢ</td>
                                    <td align="left" style="min-width:60px">TK CÓ</td>
                                    <td align="right" style="min-width:100px">SỐ TIỀN</td>
                                    <td align="left" style="min-width:115px">ĐỐI TƯỢNG</td>
                                    <td align="left" style="min-width:134px">TÊN ĐỐI TƯỢNG</td>
                                    <td align="left" style="min-width:120px">TK NGÂN HÀNG</td>
                                    <td align="left" style="min-width:156px">KHẾ ƯỚC VAY</td>
                                    <td align="left" style="min-width:235px;">KHOẢN MỤC CP</td>
                                    <td align="left" style="min-width:235px;">TÊN KHOẢN MỤC CP</td>
                                    <td align="left" style="min-width:235px;">ĐƠN VỊ</td>
                                    <td align="left" style="min-width:235px;">TÊN ĐƠN VỊ</td>
                                    <td align="left" style="min-width:235px;">ĐỐI TƯỢNG THCP</td>
                                    <td align="left" style="min-width:235px;">TÊN ĐỐI TƯỢNG THCP</td>
                                    <td align="left" style="min-width:235px;">CÔNG TRÌNH</td>
                                    <td align="left" style="min-width:235px;">TÊN CÔNG TRÌNH</td>
                                    <td align="left" style="min-width:235px;">ĐƠN ĐẶT HÀNG</td>
                                    <td align="left" style="min-width:235px;">HỢP ĐỒNG BÁN</td>
                                    <td align="left" style="min-width:235px;">ĐƠN MUA HÀNG</td>
                                    <td align="left" style="min-width:235px;">HỢP ĐỒNG MUA</td>
                                    <td align="left" style="min-width:235px;">MỤC THU/CHI</td>
                                    <td align="left" style="min-width:235px;">TÊN MỤC THU/CHI</td>
                                    <td align="left" style="min-width:235px;">MÃ THỐNG KÊ</td>
                                    <td align="left" style="min-width:235px;">TÊN MÃ THỐNG KÊ</td>
                                    <td align="left" style="min-width:235px;">CP KHÔNG HỢP LÝ</td>
                                    <td align="left" style="min-width:235px;">TRƯỜNG MỞ RỘNG 1</td>
                                    <td align="left" style="min-width:235px;">TRƯỜNG MỞ RỘNG 2</td>
                                    <td align="left" style="min-width:235px;">TRƯỜNG MỞ RỘNG 3</td>
                                    <td align="left" style="min-width:235px;">TRƯỜNG MỞ RỘNG 4</td>
                                    <td align="left" style="min-width:235px;">TRƯỜNG MỞ RỘNG 5</td>
                                    <td class="item-cn">
                                        <div class="cn-edit">
                                           
                                           <span class="mi-delete-table" >
                                           
                                           </span>
                                       </div>
                                    </td>                    
                                </tr>
                                <tr>   
                                    <td align="center" class="thead1" style="min-width:20px">
                                        1
                                    </td>
                                    <td align="left" class="thead2" style="min-width:120px">DIỄN GIẢI</td>
                                    <td align="left" style="min-width:180px">TK NỢ</td>
                                    <td align="left" style="min-width:60px">TK CÓ</td>
                                    <td align="right" style="min-width:100px">SỐ TIỀN</td>
                                    <td align="left" style="min-width:115px">ĐỐI TƯỢNG</td>
                                    <td align="left" style="min-width:134px">TÊN ĐỐI TƯỢNG</td>
                                    <td align="left" style="min-width:120px">TK NGÂN HÀNG</td>
                                    <td align="left" style="min-width:156px">KHẾ ƯỚC VAY</td>
                                    <td align="left" style="min-width:235px;">KHOẢN MỤC CP</td>
                                    <td align="left" style="min-width:235px;">TÊN KHOẢN MỤC CP</td>
                                    <td align="left" style="min-width:235px;">ĐƠN VỊ</td>
                                    <td align="left" style="min-width:235px;">TÊN ĐƠN VỊ</td>
                                    <td align="left" style="min-width:235px;">ĐỐI TƯỢNG THCP</td>
                                    <td align="left" style="min-width:235px;">TÊN ĐỐI TƯỢNG THCP</td>
                                    <td align="left" style="min-width:235px;">CÔNG TRÌNH</td>
                                    <td align="left" style="min-width:235px;">TÊN CÔNG TRÌNH</td>
                                    <td align="left" style="min-width:235px;">ĐƠN ĐẶT HÀNG</td>
                                    <td align="left" style="min-width:235px;">HỢP ĐỒNG BÁN</td>
                                    <td align="left" style="min-width:235px;">ĐƠN MUA HÀNG</td>
                                    <td align="left" style="min-width:235px;">HỢP ĐỒNG MUA</td>
                                    <td align="left" style="min-width:235px;">MỤC THU/CHI</td>
                                    <td align="left" style="min-width:235px;">TÊN MỤC THU/CHI</td>
                                    <td align="left" style="min-width:235px;">MÃ THỐNG KÊ</td>
                                    <td align="left" style="min-width:235px;">TÊN MÃ THỐNG KÊ</td>
                                    <td align="left" style="min-width:235px;">CP KHÔNG HỢP LÝ</td>
                                    <td align="left" style="min-width:235px;">TRƯỜNG MỞ RỘNG 1</td>
                                    <td align="left" style="min-width:235px;">TRƯỜNG MỞ RỘNG 2</td>
                                    <td align="left" style="min-width:235px;">TRƯỜNG MỞ RỘNG 3</td>
                                    <td align="left" style="min-width:235px;">TRƯỜNG MỞ RỘNG 4</td>
                                    <td align="left" style="min-width:235px;">TRƯỜNG MỞ RỘNG 5</td>
                                    <td class="item-cn">
                                        <div class="cn-edit">
                                           
                                           <span class="mi-delete-table" >
                                           
                                           </span>
                                       </div>
                                    </td>                    
                                </tr>
                            </tbody>
                            
                        </table>
                    </div>
                </div>
                <div class="main-child">
                    <div class="ms-child">
                        <button class="btn-addRow">Thêm dòng</button>
                        <button class="btn-deleteRow">Xóa hết dòng</button>
                    </div>
                    <div class="ms-child-2">
                        <div class="merge-child">
                            <div class="mi-attach"></div>
                            <div>Đính kèm</div>
                        </div>
                        <div class="max-size-upload">Dung lượng tối đa 5MB</div>
                    </div>
                    <input type="text" readonly name="" class="upload-main" id="" placeholder="Kéo/thả tệp vào đây hoặc bấm vào đây">
                </div>
            </div>
        </div>
        <div class="footer-layout">
            <div class="f-pog">
                <button class="f-pog-l closeEmployee white unhover" tabindex="17" id="cancel-add-form" title="Hủy(Ctrl F9)" @click="onCancelModal">{{Resource.BUTTON.HUY}}</button>
                <div class="f-pog-r">
                    <button  class="cat-pog white unhover" tabindex="18" id="cancel-add-form" title="Cất" type="submit" >Cất </button>
                    <button  tabindex="19" :id="idButton" :title="titleButton" class="add-pog flex1" type="submit" :class="[id == 'edit-form']">Cất và In</button>
                </div>
            </div>
        </div>
</div>
</template>
<script>
// import TheInputVue from "./TheInput.vue";
// import CompTabs from './CompTabs.vue';
// import TheButton from "../../components/TheButton.vue";
import TheDropdown from "../../components/TheDropdown.vue";
// import TheRadio from "../../components/TheRadio.vue";
// import TheDialog from '../../components/TheDialog.vue'
import Resource from "@/Resource/Resource";
import keyCode from "@/enum/keyCode";
// import TheInput from "@/components/TheInput.vue";
import {RepositoryFactory} from '@/Repository/RepositoryFactory';
 const EmployeeRepository = RepositoryFactory.get('Employees');
//  const DepartmentRepository = RepositoryFactory.get('Departments');
import DepartmentRepository from "@/Repository/DepartmentRepository";


export default {
    name: "ReceiptDetail",
    components: {
        
        TheDropdown,
        
        
    },
    props: {
        id: String,
        employeeCode: String,
        employeeID: String,
        
    },
    beforeMount() {
        // Xử lý UI form thêm hoặc sửa trước khi mở form
        this.handleUI();
        // Xử lý Lấy mã nhân viên mới
        this.getNewEmployeeCode();
        // Lấy danh sách phòng ban đưa lên UI
        this.getDepartment();
        // Lấy dữ liệu nhân viên đưa vào trong form
        this.userInfo(this.employeeCode);
    },
    // Tự động focus mã nhân viên khi form mở
    mounted() {
        // this.$refs.input.focus();
        
        this.employeeOld = structuredClone(this.employee);
    },
    
    methods: {
        /**
         * Xử lý tabcomponents
         * Author : Đinh Công Trứ (2/11/2022)
         */
         setActiveTab(index){
            this.tabActive = index;
         },
        /**
         * Xử lý phím tắt trong MODAL
         * Author : Đinh Công Trứ (2/11/2022)
         * 
         */
        handleshortcuts(e){
            if(e.keyCode == keyCode.keyCode.esc){
                this.onCloseModal();
            }
            if( e.ctrlKey && e.keyCode == keyCode.keyCode.cancel){
                this.onCancelModal();
            }
            if(e.ctrlKey && e.keyCode == keyCode.keyCode.save){
                this.save();
            }
           

        },
        /**
         * Thực hiện xử lý tab order
         **  Author: Đinh Công Trứ(2/11/2022)
         */
        // tabOrder() {
        //     this.$refs.input.focus();
        // },
        /**
         * Thực hiện xử lý đóng form
         **  Author: Đinh Công Trứ(2/11/2022)

         */
        onCloseModal() {
                   

            try {
                if(JSON.stringify(this.employee) == JSON.stringify(this.employeeOld)){
                    // this.$parent.isDialog = false;
                  
                    this.$emit("closeModal");
                }else{
                    this.isWarring = true;
     
                }
            } catch (error) {
                console.log(error);
            }

        },
        onCancelModal() {
    
            this.$emit("closeModalRece");
      
        },
        /**
         * Thực hiện xử lý đóng dialog
         **  Author: Đinh Công Trứ(2/11/2022)
         */
        onCloseDialog() {
            this.isError = !this.isError;
            
        },
        onCancelDialog(){
            this.isWarring = !this.isWarring;
            
        },
        /**
         * Thực hiện xử lý UI form thêm nhân viên, sửa nhân viên
         **  Author: Đinh Công Trứ(2/11/2022)
         */
        handleUI() {
            if (this.id == "add-vendor") {
                this.title = "Thông tin nhà cung cấp";
                this.idButton = "save";
                this.titleButton = Resource.BUTTON.CATTHEM;
            }
            // Thêm nhân viên
            if (this.id == "add-form") {
                this.title = Resource.TITLE.AddForm;
                this.idButton = "save";
                this.titleButton = Resource.BUTTON.CATTHEM;
            }
            // Sửa nhân viên
            if (this.id == "edit-form") {
                this.title = Resource.TITLE.EditForm;
                this.idButton = "edit";
                this.titleButton = Resource.BUTTON.CATSUA;
            }
            // Nhân bản nhân viên
            if (this.id == "nhanban-form") {
                this.title = Resource.TITLE.AddForm;
                this.idButton = "edit";
                this.titleButton = Resource.BUTTON.CATNHAN;
            }
        },
        /**
         * Thực hiện lấy dữ giới tính
         **  Author: Đinh Công Trứ(2/11/2022)
         */
        getGender(event) {
            this.employee.gender = event;
        },
        /**
         * Thực hiện lấy dữ liệu đơn vị gồm id và tên
         **  Author: Đinh Công Trứ(2/11/2022)
         */
         getDepartmentId(event) {
            this.employee.departmentID = event;
    
        },
        getDepartmentName(event) {
            this.employee.departmentName = event;
            
            
        },
        /**
         * Thực hiện xử lý validate dữ liệu mã nhân viên
         **  Author: Đinh Công Trứ(2/11/2022)
         */
        validateEmployeeCode(code) {
            if (!code) {
                this.error.employeeCode = "form-input-error";
            } else {
                this.error.employeeCode = "";
            }
        },
        /**
         * Thực hiện xử lý validate dữ liệu tên nhân viên
         **  Author: Đinh Công Trứ(2/11/2022)
         */
        validateEmployeeName(name) {
            let me = this;
            return () => {
                
                if (!name) {
                    me.error.employeeName = "form-input-error";
                } else {
                    me.error.employeeName = "";
                }
            }
        },
        /**
         * Thực hiện validate dữ liệu khi ấn lưu
         **  Author: Đinh Công Trứ(2/11/2022)
         */
        validateForm() {
            if (!this.employee.employeeCode) {
                this.error.employeeCode = "form-input-error";
               
            } else {
                this.error.employeeCode = "";
            }
            if (!this.employee.employeeName) {
                this.error.employeeName = "form-input-error";
                this.isError = !this.isError;
                this.errorMgs = Resource.ERROR.employeeNameError;
               return false;
               
            } else {
                this.error.employeeName = "";
            }
            if (!this.employee.departmentName) {
                this.error.departmentName = "form-input-error";
                this.isError = !this.isError;
                this.errorMgs = Resource.ERROR.departmentError;
               return false;
            }

            return true;
        },
        /**
         * Thực hiện format date từ api trả về
         **  Author: Đinh Công Trứ(2/11/2022)
         */
        formatDate(date) {
            if (date) {
                var d = new Date(date),
                    month = "" + (d.getMonth() + 1),
                    day = "" + d.getDate(),
                    year = d.getFullYear();

                if (month.length < 2) month = "0" + month;
                if (day.length < 2) day = "0" + day;

                return [year, month, day].join("-");
            } else {
                return null;
            }
        },
        /**
         * Thực hiện lấy mã nhân viên mới nhất
         **  Author: Đinh Công Trứ(2/11/2022)
         */
        getNewEmployeeCode() {
            if (this.id == "add-form" || this.id == "nhanban-form" || this.id =="add-vendor") {
                const me = this;
                try{
                    EmployeeRepository.getNewEmployeeCode()
                    .then((response) => {                    
                        this.employee.employeeCode = response.data.EmployeeCode;  
                        this.employeeOld.employeeCode = response.data.EmployeeCode;                        
                    })
                    .catch((error) => {
                        if(error.response.status == 500){
                            me.isError = !me.isError;
                            me.errorMgs = Resource.ERROR.loadDataError;
                        }else {
                            me.isError = !me.isError;
                            me.errorMgs = error.response.data.userMsg;
                        }
                    });
                } catch (error){
                    console.log(error);
                }
             }
        },
        /**
         * Thực hiện lấy Danh sách đơn vị
         **  Author: Đinh Công Trứ(2/11/2022)
         */
        getDepartment() {
            const me = this;
            DepartmentRepository.getAllDepartments()
            .then((res) => {
                    this.departmentList = res.data;
                })
                .catch((error) => {
                    if(error.response.status == 500){
                            me.isError = !me.isError;
                            me.errorMgs = Resource.ERROR.filterDataError;
                        }else {
                            me.isError = !me.isError;
                            me.errorMgs = error.response.data.userMsg;
                        }
                });
        },
        /**
         * Thực hiện lấy dữ liệu nhân viên đưa ra form
         **  Author: Đinh Công Trứ(2/11/2022)
         */

        userInfo(id) {
            try {
                if (this.id == "edit-form") {
                    const me = this;
                    EmployeeRepository.getInfoEmployee(id)
                    .then((response) => {
                            // me.employeeOld = response.data.data[0];
                            me.employee = response.data.data[0];
                            me.employee.dateOfBirth = this.formatDate(
                                response.data.data[0].dateOfBirth
                            );
                 
                            me.employee.identitiDate = this.formatDate(
                                response.data.data[0].identitiDate
                            );
                            me.employeeOld=structuredClone(me.employee);
                            
                            
                        })
                        .catch((error) => {
                            if(error.response.status == 500){
                            me.isError = !me.isError;
                            me.errorMgs = Resource.ERROR.loadDataError;
                            }else {
                                me.isError = !me.isError;
                                me.errorMgs = error.response.data.userMsg;
                            }
                        });
                    
                }
                if (this.id == "nhanban-form") {                  
                    const me = this;
                    EmployeeRepository.getInfoEmployee(id)
                    .then((response) => {
                     
                     me.employee = response.data.data[0];
                        
                      
                 
                     me.employee.dateOfBirth = this.formatDate(
                         response.data.data[0].dateOfBirth
                     );
                     me.employee.identitiDate = this.formatDate(
                         response.data.data[0].identitiDate
                     );
                     me.employee.gender = response.data.data[0].gender;
                     this.employeeOld=structuredClone(this.employee); 

                     
                     
                 })
                //  this.employee.employeeCode = ""
                 this.getNewEmployeeCode()   ;
                       
                }
            } catch (error) {
                this.isError = !this.isError;
                this.errorMgs = Resource.ERROR.loadDataError;
            }
        },
        /**
         * Thực hiện Lưu dữ liệu thêm mới hoặc sửa nhân viên
         **  Author: Đinh Công Trứ(2/11/2022)
         */

        save() {
            const validate = this.validateForm();

            const me = this;
            // Thêm mới nhân viên
            if (validate && this.id == "add-vendor") {
                if (this.employee.dateOfBirth) {
                    this.employee.dateOfBirth = new Date(
                        this.employee.dateOfBirth
                    );
                }
                if (this.employee.identitiDate) {
                    this.employee.identitiDate = new Date(
                        this.employee.identitiDate
                    );
                }
                if(this.employee.gender){
                    this.employee.gender = parseInt(this.employee.gender)
                }
                try {
                    EmployeeRepository.postEmployee(this.employee)
                    .then( res =>  {
                        if(res){
                        this.$emit("closeModal");
                        me.$emit("refreshData");
                        }
                    })
                        .catch(function (error) {
                            if(error.response.status == 500){
                                me.isError = !me.isError;
                                me.errorMgs = Resource.ERROR.errorPost;
                            }else {
                                me.isError = !me.isError;
                                me.errorMgs = error.response.data.userMsg;
                            }
                        });
                } catch (error) {
                    console.log(error);
                }
            } else{
                this.isWarring =  false;
            }
            // Nhân bản nhân viên
            if (validate && this.id == "nhanban-form") {
                if (this.employee.dateOfBirth) {
                    this.employee.dateOfBirth = new Date(
                        this.employee.dateOfBirth
                    );
                }
                if (this.employee.identitiDate) {
                    this.employee.identitiDate = new Date(
                        this.employee.identitiDate
                    );
                }
                if(this.employee.gender){
                    this.employee.gender = parseInt(this.employee.gender)
                }
               try {
                EmployeeRepository.postEmployee(this.employee)
                .then( res =>  {
                        if(res){
                        this.$emit("closeModal");
                        me.$emit("refreshData");
                        }
                    })
                    .catch(function (error) {
                        if(error.response.status == 500){
                            me.isError = !me.isError;
                            me.errorMgs = Resource.ERROR.errorPost;
                        }else {
                            me.isError = !me.isError;
                            me.errorMgs = error.response.data.userMsg;
                        }
                    });
               } catch (error) {
                    console.log(error);
               }

            } else{
                this.isWarring =  false;
            }
            // Sửa nhân viên
            if ( validate && this.id == "edit-form") {
                if (this.employee.dateOfBirth) {
                    this.employee.dateOfBirth = new Date(
                        this.employee.dateOfBirth
                    );
                }
                if (this.employee.identitiDate) {
                    this.employee.identitiDate = new Date(
                        this.employee.identitiDate
                    );
                }

                if(this.employee.gender){
                    this.employee.gender = parseInt(this.employee.gender)
                }
               try {
                EmployeeRepository.updateEmployee(this.employeeID,this.employee)
                .then(() => {
                        this.$emit("closeModal");
                        me.$emit("refreshData");
                    })
                    .catch((error) => {
                        if(error.response.status == 500){
                            me.isError = !me.isError;
                            me.errorMgs = Resource.ERROR.errorEdit;
                        }else {
                            me.isError = !me.isError;
                            me.errorMgs = error.response.data.userMsg;
                        }
                    });
               } catch (error) {
                    console.log(error);
               }
               
            } else{
                this.isWarring =  false;
            }
        },
        /**
         * Thực hiện xử lý Lưu nhân viên khi ấn button thêm và cất
         **  Author: Đinh Công Trứ(2/11/2022)
         */
        saveAndAdd() {
            const validate = this.validateForm();

            const me = this;
            if (validate && this.id == "add-form") {
                if (this.employee.dateOfBirth) {
                    this.employee.dateOfBirth = new Date(
                        this.employee.dateOfBirth
                    );
                }
                if (this.employee.identitiDate) {
                    this.employee.identitiDate = new Date(
                        this.employee.identitiDate
                    );
                }
                if(this.employee.gender){
                    this.employee.gender = parseInt(this.employee.gender)
                }
                try {
                    EmployeeRepository.postEmployee(this.employee)
                    .then(function () {
                        me.$emit("refreshData");
                        me.employee = { };
                        
                    })
                    .catch(function (error) {
                        if(error.response.status == 500){
                            me.isError = !me.isError;
                            me.errorMgs = Resource.ERROR.errorPost;
                        }else {
                            me.isError = !me.isError;
                            me.errorMgs = error.response.data.userMsg;
                        }
                    });
                } catch (error) {
                    console.log(error);
                }
               
            } else{
                this.isWarring =  false;
            }
            // nhân bản nhân VIÊN
            if (validate && this.id == "nhanban-form") {
                if (this.employee.dateOfBirth) {
                    this.employee.dateOfBirth = new Date(
                        this.employee.dateOfBirth
                    );
                }
                if (this.employee.identitiDate) {
                    this.employee.identitiDate = new Date(
                        this.employee.identitiDate
                    );
                }
                if(this.employee.gender){
                    this.employee.gender = parseInt(this.employee.gender)
                }
                try {
                EmployeeRepository.postEmployee(this.employee)
                .then(function () {
                        me.$emit("refreshData");
                        me.employee = { };
                        
                    })
                    .catch(function (error) {
                        if(error.response.status == 500){
                            me.isError = !me.isError;
                            me.errorMgs = Resource.ERROR.errorPost;
                        }else {
                            me.isError = !me.isError;
                            me.errorMgs = error.response.data.userMsg;
                        }
                    });
               } catch (error) {
                    console.log(error);
               }
   
            } else{
                this.isWarring =  false;
            }
            // Sửa nhân viên
            if (validate && this.id == "edit-form") {
                if (this.employee.dateOfBirth) {
                    this.employee.dateOfBirth = new Date(
                        this.employee.dateOfBirth
                    );
                }
                if (this.employee.identitiDate) {
                    this.employee.identitiDate = new Date(
                        this.employee.identitiDate
                    );
                }
                if(this.employee.gender){
                    this.employee.gender = parseInt(this.employee.gender)
                }
                try {
                EmployeeRepository.updateEmployee(this.employeeID,this.employee)
                .then(() => {
                        me.$emit("refreshData");
                        me.employee = { };                      
                    })
                    .catch((error) => {
                        if(error.response.status == 500){
                            me.isError = !me.isError;
                            me.errorMgs = Resource.ERROR.errorEdit;
                        }else {
                            me.isError = !me.isError;
                            me.errorMgs = error.response.data.userMsg;
                        }
                    });
               } catch (error) {
                    console.log(error);
               }
                
            } else{
                this.isWarring =  false;
            }

        },
    },
    data() {
        return {
            keyCode : keyCode,
            Resource :Resource,
            employeeOld: {},
            posts: [],
            isWarring: false,
            isDialog: false,
            error: {
                employeeCode: true,
                employeeName: true,
                departmentName: true,
            },
            employee: {},
            idButton: "",
            titleButton: "",
            title: "",
            errorMgs: false,
            isError: false,
            departmentList: {},
            radioData:{
                Male: "Nam",
                Female: "Nữ",
                Orther: "Khác"
            },
            radio:{
                Group: "Tổ chức",
                Sigle: "Cá nhân",
                
            },
            MsTabs:[
                { name : "Thông tin liên hệ"},
                { name : "Điều khoản thanh toán"},
                { name : "Tài khoản ngân hàng"},
                { name : "Địa chỉ khác"},
                { name : "Ghi chú"},
            ],
            tabActive : 0,
        };
    },
};
</script>
<style>
@import url(../../css/main.css);
#width334{
    width: 334px !important;
}
#w100{
    width: 100px !important;
}
.white{
    color: white !important;
}
.flex1{
    display: flex !important;
    justify-content: center !important;
    align-content: center !important;
}
.unhover{
    background: #38393d !important;
}
.unhover:focus{
    border: 1px solid #8d9096 !important;
}
.sochi{
    width: calc(60% + 2px);
    height: 28px;
    padding-left: 12px;
}
</style>