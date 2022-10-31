
var btnHHI_LoadLists = document.getElementById("btnHHI_LoadLists");
var result = document.getElementById("result");
var txtBarcode = document.getElementById('txtBarcode');

const cylinderSizes = [];
const cylinderStatuses = [];
const gases = [];
const baseUrl = `http://localhost:1011`;

//Factory Function
function createGas(id, gasName) {
    return {
        id: id
        , gasName: gasName
        , default: 'N'
    }
};

// Constructor Function
function CylinderStatus(statusId, statusName) {
    this.statusId = statusId;
    this.statusName = statusName;
    this.default = 'N'
};
function CylinderSize(id, cylSizeId) {
    this.id = id;
    this.cylSizeId = cylSizeId;
    this.default = 'N'
}
function Cylinder(barcode, itemNum, cylStatus, statusName) {
    this.barcode = barcode;
    this.itemNum = itemNum;
    this.cylStatus = cylStatus;
    this.statusName = statusName;
}

// page load - load default lists
document.addEventListener("DOMContentLoaded", function () {
    var url = `${baseUrl}/api/HH_Inventory/HHInventory_LoadLists`;
    var obj = FetchPost(url, null);
    obj.then((res) => {
        res['cylinderStatues'].forEach((i, k) => {
            const status = new CylinderStatus(res['cylinderStatues'][k].statusId, res['cylinderStatues'][k].statusName);
            cylinderStatuses.push(status);
        });
        res['gases'].forEach((i, k) => {
            const gas = createGas(res['gases'][k].gasId, res['gases'][k].gasName);
            gases.push(gas);
        });
        res['cylinderSizes'].forEach((i, k) => {
            const cylinderSize = new CylinderSize(res['cylinderSizes'][k].id, res['cylinderSizes'][k].cylSizeId);
            cylinderSizes.push(cylinderSize);
        });
    })
});

btnHHI_GetCylinder.addEventListener('click',
    (e) => {
        let barcode = txtBarcode.value;
        let cylinder;
        callDB();
        var url = `${baseUrl}/api/HH_Inventory/HHInventory_GetCylinder?barcode=${barcode}`;
        var obj = FetchPost(url, null);
        obj.then((res) => {
            cylinder = new Cylinder(res[0].barcode, res[0].itemNum, res[0].cylStatus, res[0].statusName);
            result.innerHTML = cylinder.itemNum;
        });
    });

async function FetchPost(url, obj) {
    let options = {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
            , 'Accept': 'application/json'
        }
        , body: JSON.stringify(obj)
    };
    const response = await fetch(url, options);
    const data = await response.json();
    return data;
}

function makeLoad() {
    console.log(`Making Request to Load`);
    var url = `${baseUrl}/api/HH_Inventory/HHInventory_LoadLists`;
    var obj = FetchPost(url, null);
    return obj;
}

function processCylinder(response) {
    console.log('Processing Cylinder response');
    let barcode = txtBarcode.value;
    var url = `${baseUrl}/api/HH_Inventory/HHInventory_GetCylinder?barcode=${barcode}`;
    var obj = FetchPost(url, null);
    return obj;
}

async function callDB() {
    const response = await makeLoad();
    console.log(`Load Response received ${response}`);
    const process = await processCylinder(response);
    console.log(`Complete : ${process}`);
}

function makeRequest(location) {
    return new Promise((resolve, reject) => {
        console.log(`Making Request to ${location}`);
        if (location === 'Google') {
            resolve('Google says hi');
        } else {
            reject('We can only talk to Google');
        }
    })
}

function processRequest(response) {
    return new Promise((resolve, reject) => {
        console.log('Processing response');
        resolve(`Extra Information ${response}`);
    });
}

async function doWork() {
    const response = await makeRequest('Google');
    console.log('Response received');
    const process = await processRequest(response);
    console.log(process);
}