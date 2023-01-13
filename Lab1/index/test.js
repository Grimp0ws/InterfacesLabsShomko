
function readFile(object) {
    const file = object.files[0];
    const reader = new FileReader();
    reader.onload = function() {
        document.getElementById('out').innerHTML = reader.result
    }
    reader.readAsText(file)
}
function download(name, type) {
    const text = document.getElementById('out').innerText;
    const a = document.getElementById("a");
    const file = new Blob([text], {type: type});
    a.href = URL.createObjectURL(file);
    a.download = name;
    console.log(text);
}


function undo() {
    out.focus();
    document.execCommand("undo");
}
function size5(){
    document.execCommand('fontsize', false, "5")
}
function size4(){
    document.execCommand('fontsize', false, "4")
}
function size3(){
    document.execCommand('fontsize', false, "3")
}
function colorRed(){
    document.execCommand("foreColor", true, "red")
}
function colorBlue(){
    document.execCommand("foreColor", true, "blue")
}
function bold(){
    document.execCommand('bold', false)
}
function underline(){
    document.execCommand("underline");
}
function italic(){
    document.execCommand('italic', false)
}
function insertOrderedList(){
    document.execCommand('insertOrderedList', false)
}
function insertUnorderedList(){
    document.execCommand('insertUnorderedList', false)
}
function justifyCenter(){
    document.execCommand('justifyCenter', false)
}
function printDiv(divName) {
    const printContents = document.getElementById(divName).innerHTML;
    const originalContents = document.body.innerHTML;
    document.body.innerHTML = printContents;
    window.print();
    document.body.innerHTML = originalContents;
}
function justifyRight() {
    document.execCommand('justifyRight', false)
}
function justifyLeft() {
    out.focus();
    document.execCommand('justifyLeft', false)
}






