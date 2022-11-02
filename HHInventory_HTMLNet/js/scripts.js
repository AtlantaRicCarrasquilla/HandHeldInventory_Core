
const btnHHI_UpdateCylinder = document.getElementById("btnHHI_UpdateCylinder");
const result = document.getElementById("result");
const txtBarcode = document.getElementById('txtBarcode');
const spnItemNum = document.getElementById('spnItemNum');
const spnStatusName = document.getElementById("spnStatusName");
const spnCustId = document.getElementById("spnCustId");
const spnCustOwned = document.getElementById("spnCustOwned");
const spnBusinessAddr = document.getElementById("spnBusinessAddr");
const spnGasNames = document.getElementById("spnGasName");
const spnNets = document.getElementById("spnNet");
const spnNetBoilOff = document.getElementById("spnNetBoilOff");
const spnPurity = document.getElementById("spnPurity");
const spnMixedGasPrimeCompName = document.getElementById("spnMixedGasPrimeCompName");
const selCylinderSizes = document.getElementById("selCylinderSizes");

let cylinder;

// Get the modal
const modal = document.getElementById("myModal");

const cylinderSizes = [];
const cylinderStatuses = [];
const elementsInnerHTML = [spnItemNum, spnStatusName, spnCustId, spnCustOwned, spnBusinessAddr, spnGasNames, spnNets, spnNetBoilOff, spnPurity, spnMixedGasPrimeCompName];
const gases = [];
const baseUrl = `http://localhost:1011`;

// Get the <span> element that closes the modal
const span = document.getElementsByClassName("close")[0];
// When the user clicks on <span> (x), close the modal
span.onclick = function () {
    modal.style.display = "none";
}

// When the user clicks anywhere outside of the modal, close it
window.onclick = function (event) {
    if (event.target == modal) {
        modal.style.display = "none";
    }
}
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
function Cylinder(barcode, itemNum, cylStatus, statusName, custOwned, custId, businessAddr, gasId, gasName, net, netBoilOff, mixedGasPrimeCompId, mixedGasPrimeCompName, purity, outFlag, outMessage) {
    this.barcode = barcode;
    this.itemNum = itemNum;
    this.cylStatus = cylStatus;
    this.statusName = statusName;
    this.custOwned = custOwned;
    this.custId = custId;
    this.businessAddr = businessAddr;
    this.gasId = gasId;
    this.gasName = gasName;
    this.net = net;
    this.netBoilOff = netBoilOff;
    this.mixedGasPrimeCompId = mixedGasPrimeCompId;
    this.mixedGasPrimeCompName = mixedGasPrimeCompName;
    this.purity = purity;
    this.outFlag = outFlag;
    this.outMessage = outMessage;
}

function sizeMe() {
    const sizes = cylinderSizes.map((e) => {
        return e.cylSizeId;
    });
    return sizes;
}

let mySizes;
//cylinderSizes.forEach((i, k) => {
//    //const cylinderSize = new CylinderSize(res['cylinderSizes'][k].id, cylinderSizes[k].cylSizeId);
//    mySizes.push(cylinderSizes[k].cylSizeId);
//});

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
        mySizes = res['cylinderSizes'].map((i, k) => {
            return i.cylSizeId;
        });

        autocomplete(document.getElementById("myInput"), mySizes);
        autocomplete(document.getElementById("txtCylinderSizes"), mySizes);
    });

});

txtBarcode.addEventListener('focusout',
    (e) => {
        let barcode = txtBarcode.value;
        //clear form
        elementsInnerHTML.forEach((e, i) => {
            e.innerHTML = "";
        });
        callDB();
        GetCylinder(barcode);        
    });

async function GetCylinder(barcode) {
    var url = `${baseUrl}/api/HH_Inventory/HHInventory_GetCylinder?barcode=${barcode}`;
    var obj = FetchPost(url, null);
    obj.then((res) => {
        cylinder = new Cylinder(res[0].barcode, res[0].itemNum, res[0].cylStatus, res[0].statusName, res[0].custOwned, res[0].custId, res[0].businessAddr, res[0].gasId, res[0].gasName, res[0].net, res[0].netBoilOff, res[0].mixedGasPrimeCompId, res[0].mixedGasPrimeCompName, res[0].purity, res[0].outFlag, res[0].outMessage);
        if (cylinder.outFlag !== 100) {
            spnItemNum.innerHTML = cylinder.itemNum;
            spnStatusName.innerHTML = cylinder.statusName;
            spnCustId.innerHTML = cylinder.custId;
            spnCustOwned.innerHTML = cylinder.custOwned;
            spnBusinessAddr.innerHTML = cylinder.businessAddr;
            spnGasName.innerHTML = cylinder.gasId === 0 ? 'Empty' : cylinder.gasName;
            spnNets.innerHTML = cylinder.net;
            spnNetBoilOff.innerHTML = cylinder.netBoilOff;
            spnPurity.innerHTML = cylinder.purity;
            spnMixedGasPrimeCompName.innerHTML = cylinder.mixedGasPrimeCompId === 0 ? 'Not Mixed Gas' : cylinder.mixedGasPrimeCompName;
        }
        result.innerHTML = `${barcode} : Flag : ${cylinder.outFlag} : Message : ${cylinder.outMessage}`;
    });
}
function LoadPageArrays() {

}
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

btnHHI_UpdateCylinder.addEventListener('click',
    (e) => {
        modal.style.display = "block";
        modalText.innerHTML = cylinder.barcode;
        let defaultOpt = new Option("Pick Cylinder Size", 0);
        selCylinderSizes.add(defaultOpt, 0)
        let currentOpt = new Option(cylinder.itemNum, cylinder.itemNum);
        selCylinderSizes.add(currentOpt, 1);
        cylinderSizes.forEach((e) => {
            let newOption = new Option(e.cylSizeId, e.cylSizeId);
            selCylinderSizes.add(newOption, undefined);
        });;
    });

selCylinderSizes.addEventListener('change', (self) => {
    let el = self.target;
    let value = el.options[el.selectedIndex].value;
    let txt = el.options[el.selectedIndex].text;
    console.log(`element : ${el} value : ${value} txt : ${txt}`);
})