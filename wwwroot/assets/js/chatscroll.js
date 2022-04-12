"use strict";

export function InboxAutoScrolling() {

    var container = document.querySelector('.messages')

    container.maxScrollTop = container.scrollHeight - container.offsetHeight

    if (container.maxScrollTop - container.scrollTop <= container.offsetHeight) {
        // We can scroll to the bottom.
        // Setting scrollTop to a high number will bring us to the bottom.
        // setting its value to scrollHeight seems a good idea, because
        // scrollHeight is always higher than scrollTop.
        // However we could use any value (e.g. something like 99999 should be ok) 
        container.scrollTop = container.scrollHeight
        console.log("I just scrolled to the bottom!")

    } else {
        console.log("I won't scroll: you're way too far from the bottom!\n" +
            "You should maybe alert the user that he received a new message.\n" +
            "If he wants to scroll at this point, he must do it manually.")
    }

}