﻿!function () {
    "use strict";

    function n(t) {
        if (void 0 === t) throw new Error('Pathformer [constructor]: "element" parameter is required');
        if (t.constructor === String && !(t = document.getElementById(t))) throw new Error('Pathformer [constructor]: "element" parameter is not related to an existing ID');
        if (!(t instanceof window.SVGElement || t instanceof window.SVGGElement || /^svg$/i.test(t.nodeName))) throw new Error('Pathformer [constructor]: "element" parameter must be a string or a SVGelement');
        this.el = t, this.scan(t)
    }

    var r, e, t, p;

    function i(t, e, n) {
        r(), this.isReady = !1, this.setElement(t, e), this.setOptions(e), this.setCallback(n), this.isReady && this.init()
    }

    n.prototype.TYPES = ["line", "ellipse", "circle", "polygon", "polyline", "rect"], n.prototype.ATTR_WATCH = ["cx", "cy", "points", "r", "rx", "ry", "x", "x1", "x2", "y", "y1", "y2"], n.prototype.scan = function (t) {
        for (var e, n, r, i = t.querySelectorAll(this.TYPES.join(",")), a = 0; a < i.length; a++) n = (0, this[(e = i[a]).tagName.toLowerCase() + "ToPath"])(this.parseAttr(e.attributes)), r = this.pathMaker(e, n), e.parentNode.replaceChild(r, e)
    }, n.prototype.lineToPath = function (t) {
        var e = {}, n = t.x1 || 0, r = t.y1 || 0, i = t.x2 || 0, a = t.y2 || 0;
        return e.d = "M" + n + "," + r + "L" + i + "," + a, e
    }, n.prototype.rectToPath = function (t) {
        var e = {}, n = parseFloat(t.x) || 0, r = parseFloat(t.y) || 0, i = parseFloat(t.width) || 0,
            a = parseFloat(t.height) || 0;
        if (t.rx || t.ry) {
            var o = parseInt(t.rx, 10) || -1, s = parseInt(t.ry, 10) || -1;
            o = Math.min(Math.max(o < 0 ? s : o, 0), i / 2), s = Math.min(Math.max(s < 0 ? o : s, 0), a / 2), e.d = "M " + (n + o) + "," + r + " L " + (n + i - o) + "," + r + " A " + o + "," + s + ",0,0,1," + (n + i) + "," + (r + s) + " L " + (n + i) + "," + (r + a - s) + " A " + o + "," + s + ",0,0,1," + (n + i - o) + "," + (r + a) + " L " + (n + o) + "," + (r + a) + " A " + o + "," + s + ",0,0,1," + n + "," + (r + a - s) + " L " + n + "," + (r + s) + " A " + o + "," + s + ",0,0,1," + (n + o) + "," + r
        } else e.d = "M" + n + " " + r + " L" + (n + i) + " " + r + " L" + (n + i) + " " + (r + a) + " L" + n + " " + (r + a) + " Z";
        return e
    }, n.prototype.polylineToPath = function (t) {
        var e, n, r = {}, i = t.points.trim().split(" ");
        if (-1 === t.points.indexOf(",")) {
            var a = [];
            for (e = 0; e < i.length; e += 2) a.push(i[e] + "," + i[e + 1]);
            i = a
        }
        for (n = "M" + i[0], e = 1; e < i.length; e++) -1 !== i[e].indexOf(",") && (n += "L" + i[e]);
        return r.d = n, r
    }, n.prototype.polygonToPath = function (t) {
        var e = n.prototype.polylineToPath(t);
        return e.d += "Z", e
    }, n.prototype.ellipseToPath = function (t) {
        var e = {}, n = parseFloat(t.rx) || 0, r = parseFloat(t.ry) || 0, i = parseFloat(t.cx) || 0,
            a = parseFloat(t.cy) || 0, o = i - n, s = a, h = parseFloat(i) + parseFloat(n), l = a;
        return e.d = "M" + o + "," + s + "A" + n + "," + r + " 0,1,1 " + h + "," + l + "A" + n + "," + r + " 0,1,1 " + o + "," + l, e
    }, n.prototype.circleToPath = function (t) {
        var e = {}, n = parseFloat(t.r) || 0, r = parseFloat(t.cx) || 0, i = parseFloat(t.cy) || 0, a = r - n, o = i,
            s = parseFloat(r) + parseFloat(n), h = i;
        return e.d = "M" + a + "," + o + "A" + n + "," + n + " 0,1,1 " + s + "," + h + "A" + n + "," + n + " 0,1,1 " + a + "," + h, e
    }, n.prototype.pathMaker = function (t, e) {
        var n, r, i = document.createElementNS("http://www.w3.org/2000/svg", "path");
        for (n = 0; n < t.attributes.length; n++) r = t.attributes[n], -1 === this.ATTR_WATCH.indexOf(r.name) && i.setAttribute(r.name, r.value);
        for (n in e) i.setAttribute(n, e[n]);
        return i
    }, n.prototype.parseAttr = function (t) {
        for (var e, n = {}, r = 0; r < t.length; r++) {
            if (e = t[r], -1 !== this.ATTR_WATCH.indexOf(e.name) && -1 !== e.value.indexOf("%")) throw new Error("Pathformer [parseAttr]: a SVG shape got values in percentage. This cannot be transformed into 'path' tags. Please use 'viewBox'.");
            n[e.name] = e.value
        }
        return n
    }, i.LINEAR = function (t) {
        return t
    }, i.EASE = function (t) {
        return -Math.cos(t * Math.PI) / 2 + .5
    }, i.EASE_OUT = function (t) {
        return 1 - Math.pow(1 - t, 3)
    }, i.EASE_IN = function (t) {
        return Math.pow(t, 3)
    }, i.EASE_OUT_BOUNCE = function (t) {
        var e = 1 - Math.cos(t * (.5 * Math.PI)), n = Math.pow(e, 1.5), r = Math.pow(1 - t, 2);
        return 1 - r + (1 - Math.abs(Math.cos(n * (2.5 * Math.PI)))) * r
    }, i.prototype.setElement = function (e, n) {
        var t, r;
        if (void 0 === e) throw new Error('Vivus [constructor]: "element" parameter is required');
        if (e.constructor === String && !(e = document.getElementById(e))) throw new Error('Vivus [constructor]: "element" parameter is not related to an existing ID');
        if (this.parentEl = e, n && n.file) {
            r = this, t = function () {
                var t = document.createElement("div");
                t.innerHTML = this.responseText;
                var e = t.querySelector("svg");
                if (!e) throw new Error("Vivus [load]: Cannot find the SVG in the loaded file : " + n.file);
                r.el = e, r.el.setAttribute("width", "100%"), r.el.setAttribute("height", "100%"), r.parentEl.appendChild(r.el), r.isReady = !0, r.init(), r = null
            };
            var i = new window.XMLHttpRequest;
            return i.addEventListener("load", t), i.open("GET", n.file), void i.send()
        }
        switch (e.constructor) {
            case window.SVGSVGElement:
            case window.SVGElement:
            case window.SVGGElement:
                this.el = e, this.isReady = !0;
                break;
            case window.HTMLObjectElement:
                r = this, (t = function (t) {
                    if (!r.isReady) {
                        if (r.el = e.contentDocument && e.contentDocument.querySelector("svg"), !r.el && t) throw new Error("Vivus [constructor]: object loaded does not contain any SVG");
                        r.el && (e.getAttribute("built-by-vivus") && (r.parentEl.insertBefore(r.el, e), r.parentEl.removeChild(e), r.el.setAttribute("width", "100%"), r.el.setAttribute("height", "100%")), r.isReady = !0, r.init(), r = null)
                    }
                })() || e.addEventListener("load", t);
                break;
            default:
                throw new Error('Vivus [constructor]: "element" parameter is not valid (or miss the "file" attribute)')
        }
    }, i.prototype.setOptions = function (t) {
        var e = ["delayed", "sync", "async", "nsync", "oneByOne", "scenario", "scenario-sync"],
            n = ["inViewport", "manual", "autostart"];
        if (void 0 !== t && t.constructor !== Object) throw new Error('Vivus [constructor]: "options" parameter must be an object');
        if ((t = t || {}).type && -1 === e.indexOf(t.type)) throw new Error("Vivus [constructor]: " + t.type + " is not an existing animation `type`");
        if (this.type = t.type || e[0], t.start && -1 === n.indexOf(t.start)) throw new Error("Vivus [constructor]: " + t.start + " is not an existing `start` option");
        if (this.start = t.start || n[0], this.isIE = -1 !== window.navigator.userAgent.indexOf("MSIE") || -1 !== window.navigator.userAgent.indexOf("Trident/") || -1 !== window.navigator.userAgent.indexOf("Edge/"), this.duration = p(t.duration, 120), this.delay = p(t.delay, null), this.dashGap = p(t.dashGap, 1), this.forceRender = t.hasOwnProperty("forceRender") ? !!t.forceRender : this.isIE, this.reverseStack = !!t.reverseStack, this.selfDestroy = !!t.selfDestroy, this.onReady = t.onReady, this.map = [], this.frameLength = this.currentFrame = this.delayUnit = this.speed = this.handle = null, this.ignoreInvisible = !!t.hasOwnProperty("ignoreInvisible") && !!t.ignoreInvisible, this.animTimingFunction = t.animTimingFunction || i.LINEAR, this.pathTimingFunction = t.pathTimingFunction || i.LINEAR, this.delay >= this.duration) throw new Error("Vivus [constructor]: delay must be shorter than duration")
    }, i.prototype.setCallback = function (t) {
        if (t && t.constructor !== Function) throw new Error('Vivus [constructor]: "callback" parameter must be a function');
        this.callback = t || function () {
        }
    }, i.prototype.mapping = function () {
        var t, e, n, r, i, a, o, s, h, l;
        for (s = a = o = 0, e = this.el.querySelectorAll("path"), l = !1, t = 0; t < e.length; t++) if (n = e[t], !this.isInvisible(n)) {
            if (i = {
                el: n,
                length: 0,
                startAt: 0,
                duration: 0,
                isResizeSensitive: !1
            }, "non-scaling-stroke" === n.getAttribute("vector-effect")) {
                var c = n.getBoundingClientRect(), u = n.getBBox();
                h = Math.max(c.width / u.width, c.height / u.height), l = i.isResizeSensitive = !0
            } else h = 1;
            i.length = Math.ceil(n.getTotalLength() * h), isNaN(i.length) ? window.console && console.warn && console.warn("Vivus [mapping]: cannot retrieve a path element length", n) : (this.map.push(i), n.style.strokeDasharray = i.length + " " + (i.length + 2 * this.dashGap), n.style.strokeDashoffset = i.length + this.dashGap, i.length += this.dashGap, a += i.length, this.renderPath(t))
        }
        for (l && console.warn("Vivus: this SVG contains non-scaling-strokes. You should call instance.recalc() when the SVG is resized or you will encounter unwanted behaviour. See https://github.com/maxwellito/vivus#non-scaling for more info."), a = 0 === a ? 1 : a, this.delay = null === this.delay ? this.duration / 3 : this.delay, this.delayUnit = this.delay / (1 < e.length ? e.length - 1 : 1), this.reverseStack && this.map.reverse(), t = 0; t < this.map.length; t++) {
            switch (i = this.map[t], this.type) {
                case"delayed":
                    i.startAt = this.delayUnit * t, i.duration = this.duration - this.delay;
                    break;
                case"oneByOne":
                    i.startAt = o / a * this.duration, i.duration = i.length / a * this.duration;
                    break;
                case"sync":
                case"async":
                case"nsync":
                    i.startAt = 0, i.duration = this.duration;
                    break;
                case"scenario-sync":
                    n = i.el, r = this.parseAttr(n), i.startAt = s + (p(r["data-delay"], this.delayUnit) || 0), i.duration = p(r["data-duration"], this.duration), s = void 0 !== r["data-async"] ? i.startAt : i.startAt + i.duration, this.frameLength = Math.max(this.frameLength, i.startAt + i.duration);
                    break;
                case"scenario":
                    n = i.el, r = this.parseAttr(n), i.startAt = p(r["data-start"], this.delayUnit) || 0, i.duration = p(r["data-duration"], this.duration), this.frameLength = Math.max(this.frameLength, i.startAt + i.duration)
            }
            o += i.length, this.frameLength = this.frameLength || this.duration
        }
    }, i.prototype.recalc = function () {
        this.mustRecalcScale || (this.mustRecalcScale = e(function () {
            this.performLineRecalc()
        }.bind(this)))
    }, i.prototype.performLineRecalc = function () {
        for (var t, e, n, r, i, a = 0; a < this.map.length; a++) (t = this.map[a]).isResizeSensitive && (n = (e = t.el).getBoundingClientRect(), r = e.getBBox(), i = Math.max(n.width / r.width, n.height / r.height), t.length = Math.ceil(e.getTotalLength() * i), e.style.strokeDasharray = t.length + " " + (t.length + 2 * this.dashGap));
        this.trace(), this.mustRecalcScale = null
    }, i.prototype.draw = function () {
        var t = this;
        if (this.currentFrame += this.speed, this.currentFrame <= 0) this.stop(), this.reset(); else {
            if (!(this.currentFrame >= this.frameLength)) return this.trace(), void (this.handle = e(function () {
                t.draw()
            }));
            this.stop(), this.currentFrame = this.frameLength, this.trace(), this.selfDestroy && this.destroy()
        }
        this.callback(this), this.instanceCallback && (this.instanceCallback(this), this.instanceCallback = null)
    }, i.prototype.trace = function () {
        var t, e, n, r;
        for (r = this.animTimingFunction(this.currentFrame / this.frameLength) * this.frameLength, t = 0; t < this.map.length; t++) e = (r - (n = this.map[t]).startAt) / n.duration, e = this.pathTimingFunction(Math.max(0, Math.min(1, e))), n.progress !== e && (n.progress = e, n.el.style.strokeDashoffset = Math.floor(n.length * (1 - e)), this.renderPath(t))
    }, i.prototype.renderPath = function (t) {
        if (this.forceRender && this.map && this.map[t]) {
            var e = this.map[t], n = e.el.cloneNode(!0);
            e.el.parentNode.replaceChild(n, e.el), e.el = n
        }
    }, i.prototype.init = function () {
        this.frameLength = 0, this.currentFrame = 0, this.map = [], new n(this.el), this.mapping(), this.starter(), this.onReady && this.onReady(this)
    }, i.prototype.starter = function () {
        switch (this.start) {
            case"manual":
                return;
            case"autostart":
                this.play();
                break;
            case"inViewport":
                var t = this, e = function () {
                    t.isInViewport(t.parentEl, 1) && (t.play(), window.removeEventListener("scroll", e))
                };
                window.addEventListener("scroll", e), e()
        }
    }, i.prototype.getStatus = function () {
        return 0 === this.currentFrame ? "start" : this.currentFrame === this.frameLength ? "end" : "progress"
    }, i.prototype.reset = function () {
        return this.setFrameProgress(0)
    }, i.prototype.finish = function () {
        return this.setFrameProgress(1)
    }, i.prototype.setFrameProgress = function (t) {
        return t = Math.min(1, Math.max(0, t)), this.currentFrame = Math.round(this.frameLength * t), this.trace(), this
    }, i.prototype.play = function (t, e) {
        if (this.instanceCallback = null, t && "function" == typeof t) this.instanceCallback = t, t = null; else if (t && "number" != typeof t) throw new Error("Vivus [play]: invalid speed");
        return e && "function" == typeof e && !this.instanceCallback && (this.instanceCallback = e), this.speed = t || 1, this.handle || this.draw(), this
    }, i.prototype.stop = function () {
        return this.handle && (t(this.handle), this.handle = null), this
    }, i.prototype.destroy = function () {
        var t, e;
        for (this.stop(), t = 0; t < this.map.length; t++) (e = this.map[t]).el.style.strokeDashoffset = null, e.el.style.strokeDasharray = null, this.renderPath(t)
    }, i.prototype.isInvisible = function (t) {
        var e, n = t.getAttribute("data-ignore");
        return null !== n ? "false" !== n : !!this.ignoreInvisible && (!(e = t.getBoundingClientRect()).width && !e.height)
    }, i.prototype.parseAttr = function (t) {
        var e, n = {};
        if (t && t.attributes) for (var r = 0; r < t.attributes.length; r++) n[(e = t.attributes[r]).name] = e.value;
        return n
    }, i.prototype.isInViewport = function (t, e) {
        var n = this.scrollY(), r = n + this.getViewportH(), i = t.getBoundingClientRect(), a = i.height, o = n + i.top;
        return o + a * (e = e || 0) <= r && n <= o + a
    }, i.prototype.getViewportH = function () {
        var t = this.docElem.clientHeight, e = window.innerHeight;
        return t < e ? e : t
    }, i.prototype.scrollY = function () {
        return window.pageYOffset || this.docElem.scrollTop
    }, r = function () {
        i.prototype.docElem || (i.prototype.docElem = window.document.documentElement, e = window.requestAnimationFrame || window.webkitRequestAnimationFrame || window.mozRequestAnimationFrame || window.oRequestAnimationFrame || window.msRequestAnimationFrame || function (t) {
            return window.setTimeout(t, 1e3 / 60)
        }, t = window.cancelAnimationFrame || window.webkitCancelAnimationFrame || window.mozCancelAnimationFrame || window.oCancelAnimationFrame || window.msCancelAnimationFrame || function (t) {
            return window.clearTimeout(t)
        })
    }, p = function (t, e) {
        var n = parseInt(t, 10);
        return 0 <= n ? n : e
    }, "function" == typeof define && define.amd ? define([], function () {
        return i
    }) : "object" == typeof exports ? module.exports = i : window.Vivus = i
}();

function fadeInNodes(step, maxStep) {

    setTimeout(function () {
        $('.circle.step' + step).addClass('blue-bg');
        if (step < maxStep) {
            fadeInNodes(++step, maxStep)
        }
    }, 1000)
}
