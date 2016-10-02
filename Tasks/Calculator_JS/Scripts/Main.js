function $(selector){
	return document.getElementById(selector);
}
function c(val) {
	$("display").value = val;
}
function math(val){
	$("display").value += val;	
}
function e(){
	try{
		c(eval($("display").value));
	}
	catch(e){
		c('Error');
	}
}
function sqrt(){
	$("display").value = Math.sqrt($("display").value);
}