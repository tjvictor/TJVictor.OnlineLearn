function RadAjaxManager_OnRequestStart(sender, events) {
    if (window.top.OnRequestStart)
        window.top.OnRequestStart(sender, events);
}

function RadAjaxManager_OnResponseEnd(sender, events) {
    if (window.top.OnResponseEnd)
        window.top.OnResponseEnd(sender, events);
}