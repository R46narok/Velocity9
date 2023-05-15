function initTooltips() {
    $(function () {
        $('[data-toggle="tooltip"]').tooltip()
    })
}

function initDetailedWindowScroll() {
    console.log("aaaa");
    window.addEventListener('scroll', function () {
        var element = document.querySelector('#someId');
        var rect = element.getBoundingClientRect();
        element.style.top = (window.pageYOffset) + 'px';
        // element.style.left = (rect.left + window.scrollX) + 'px';

        console.log(element);
    });
}