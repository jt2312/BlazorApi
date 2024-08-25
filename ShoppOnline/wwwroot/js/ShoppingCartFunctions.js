functionMakeUpdateQtyButtonVisible(id, visible)
{

    const update = document.querySelector("button[data-itemID='"+id+"']")
    if (visible== true) {
        UpdateQtyButton.style.display = "iniline-block";

    } else {

        UpdateQtyButtons.style.display = "none";
        
    }
}