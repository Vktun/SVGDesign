// 组件处理模块
(function (componentsAttr, svg) {

    if (!svg) console.error("need svgJS")
    var attrs = {
        common: {

        },
        data: {

        }
    }
    componentsAttr.get = function (svgEl) {

        if (svgEl && svgEl.attr()) {
            var a = svgEl.attr();
            console.table(a)
            for (var i = 0,len=a.length; i < len; i++) {
                
                //if (a[i].indexOf('data-') > -1) {
                //    attrs.data[a[i].repalce("data-","")]=
                //}
            }
        }
    }

    function convert(input) {
        return parseInt(input);
    }
    componentsAttr.add = function (a, b) {
        return convert(a) + convert(b);
    }
    window.componentsAttr = componentsAttr;
})(window.componentsAttr || {}, SVG);

//calculator.remain(4,3)