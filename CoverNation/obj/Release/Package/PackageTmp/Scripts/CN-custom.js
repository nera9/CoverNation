//Function for setting hover effect of parsed items
function setHoverEffect(hoverBox, playBtn) {
    var playlistContent = document.getElementsByClassName(hoverBox);
    var playBtn = document.getElementById(playBtn);
    for (var i = 0; i < playlistContent.length; i++) {
        playlistContent[i].addEventListener("mouseover", PlaylistMouseOver, false);
        playlistContent[i].addEventListener("mouseout", PlaylistMouseOut, false);


    }

}
function PlaylistMouseOver() {
    this.getElementsByTagName("a")[0].setAttribute("style", "display:block !important;")
}
function PlaylistMouseOut() {
    this.getElementsByTagName("a")[0].setAttribute("style", "display:none !important;")
}
function openVideoModal() {
    $("#testVideoModal").show();
}
function onBegin() {
    $("#infinity-loader").show();
}
function onComplete() {
    $("#infinity-loader").hide();
}
function reload() {
    location.reload();
}

//======= PROFILE ======== 
// SUBSCRIBE
function subscribeMsg() {
    if ($("#SubscribeBtn").text() == "+ Subscribe") {
        $("#SubscribeBtn").text("- Unsubscribe");
        $("#SubscribeBtn").attr("id", "UnsubscribeBtn");
        showActionMessage("Successfully subscribed!");
    }
    else {
        $("#UnsubscribeBtn").text("+ Subscribe");
        $("#UnsubscribeBtn").attr("id", "SubscribeBtn");
        showActionMessage("Successfully unsubscribed!");

    }
}

function showActionMessage(message) {
    $("#actionMessage").text(message);
    $("#actionMessage").show("slide", { direction: "left" }, 1000);
    $("#actionMessage").delay(1200).hide("slide", { direction: "left" }, 1000);
}

//COVER MANAGMENT 
function clearAddCoverFields() {
    $("#CoverName").val('');
    $("#VideoURL").val('');
}


//COVER FEED
function clearCommentInput() {
    var searchInputs = $('.commentTxt');
    for (var i = 0; i < searchInputs.length; i++) {
        $(searchInputs).eq(i).val('');
    }
}
function openCommentSection(id) {
    $("#commentContainer-" + id).toggle();
}

