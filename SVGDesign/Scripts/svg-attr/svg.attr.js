// 组件处理模块
(function (componentsAttr, svg) {

    if (!svg) console.error("need svgJS");
    var attrs = {
        basic: {

        },
        data: {

        }
    };
    componentsAttr.get = function (svgEl, type) {
        var res = "";
        if (svgEl && svgEl.attr()) {
            var a = svgEl.attr();
            for (var key in a) {
                console.log(key)
                if (key.indexOf("data-") > -1) {
                    if (key === "data-object") {
                        res = type === 'html' ? getTpl(a[key]) : a[key] ;
                        return res;
                    }

                } else {
                    $("input[name='" + key + "']").val(a[key]);
                }
            }
        }
    };

    function getTpl(dataObj) {
        var t = "";
        if (dataObj) {
            for (var key in dataObj) {
                t += '<div class="input-group input-group-sm"><span class="input-group-addon">' + dataObj[key] + '</span >' +
                    '<input type="text" class="form-control" placeholder="cursor:pointer;fill:#f03;" value="' + dataObj[key].value + '">' +
                    '</div>';
            }
        }
        return t;
    }

    window.componentsAttr = componentsAttr;
})(window.componentsAttr || {}, SVG);

//calculator.remain(4,3)