var blazorInterop = blazorInterop || {};

blazorInterop.elementReference = null;

blazorInterop.registerEscListener = function (element) {
    blazorInterop.elementReference = element;
    document.addEventListener('keydown', blazorInterop.onKeyDownEvent);
};

blazorInterop.unRegisterEscListener = function () {
    document.removeEventListener('keydown', blazorInterop.onKeyDownEvent);
    blazorInterop.elementReference = null;
};

blazorInterop.onKeyDownEvent = function (args) {
    if (blazorInterop.elementReference != null && args.key == "Escape") {
        blazorInterop.elementReference.invokeMethodAsync('JsInvokeEscPressedAsync');
    }
};

blazorInterop.applicationCulture = {
    get: () => window.localStorage['ApplicationCulture'],
    set: (value) => window.localStorage['ApplicationCulture'] = value,
    getCookie: () => getCookie('.AspNetCore.Culture'),
    setCookie: (value) => setCookie(value),
    check: () => checkCulture(),
};

//blazorInterop.appCulture = {
//    get: () => window.localStorage['AppLanguage'],
//    set: (value) => window.localStorage['AppLanguage'] = value
//};

blazorInterop.scrollToEnd = function scrollToEnd(element) {
    //console.log("ovo");
    //console.log(element);
    //console.log("ovo");
    //element.scrollTop = element.scrollHeight;
    element.scrollIntoView();
}

function getCookie(cname) {
    let name = cname + "=";
    let decodedCookie = decodeURIComponent(document.cookie);
    let ca = decodedCookie.split(';');
    for (let i = 0; i < ca.length; i++) {
        let c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}

function setCookie(cvalue) {
    const d = new Date();
    d.setTime(d.getTime() + (90 * 24 * 60 * 60 * 1000));
    let expires = "expires=" + d.toUTCString();
    document.cookie = ".EInsp2.Culture =" + cvalue + ";" + expires + ";path=/";
}

function checkCulture() {
    var blazor = getCookie('.EInsp2.Culture');
    if (blazor[0] === '1')
        return "1";
    else return "0";
}

blazorInterop.saveAsFile = function (filename, bytesBase64) {
    var link = document.createElement('a');
    link.download = filename;
    //link.href = "data:application/pdf;base64," + bytesBase64;
    link.href = "data:application/octet-stream;base64," + bytesBase64;
    document.body.appendChild(link); // Needed for Firefox
    link.click();
    document.body.removeChild(link);
}


function base64ToBlob(base64, type = "application/octet-stream") {
    const binStr = atob(base64);
    const len = binStr.length;
    const arr = new Uint8Array(len);
    for (let i = 0; i < len; i++) {
        arr[i] = binStr.charCodeAt(i);
    }
    return new Blob([arr], { type: type });
}

window.methods = {
    openpdf: function (filename, base64) {
        const blob = base64ToBlob(base64, 'application/pdf');
        const url = URL.createObjectURL(blob);
        const pdfWindow = window.open("");
        //pdfWindow.document.write("<iframe width='100%' height='100%' src='" + url + "'></iframe>");
        //pdfWindow.document.write("<embed width='100%' height='100%' src='" + url + "'/>");

        let html = '';

        html += '<html>';
        html += '<head><title>' + filename + '</title></head>';
        html += '<body style="margin:0!important">';
        html += "<embed width='100%' height='100%' src='" + url + "'/>";
        html += '</body>';
        html += '</html>';

        setTimeout(() => {
            pdfWindow.document.write(html);
        }, 0);
        
    }
}

/*
 function (filename, bytesBase64) {
        const win = window.open("", "_blank");
        let html = '';

        html += '<html>';
        html += '<head><title>' + filename + '</title></head>';
        html += '<body style="margin:0!important">';
        html += '<embed width="100%" height="100%" src="data:application/pdf;base64,' + bytesBase64 + '" type="application/pdf" />';
        html += '</body>';
        html += '</html>';

        setTimeout(() => {
            win.document.write(html);
        }, 0);
    }
 */