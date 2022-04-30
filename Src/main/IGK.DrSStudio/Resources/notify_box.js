function getBoxSize(){
	var s = "";
	var t = $igk('#notifybox').getItemAt(0).getSize();
	if (t.w && t.h)
		s = t.w+ ";"+t.h;	
	else 
		s = "0;0";
	return s;
}
function close(){
	igk.ext.call('close');
}

ns_igk.ready(function(){
	q = $igk("#notifybox").getItemAt(0);
	q.add("a")
	.addClass("dispib loc_t loc_r posab")
	.setCss({marginRight:"16px", marginTop:"8px", cursor:"pointer"})
	.reg_event("click", function(evt){
		evt.preventDefault();
		close();
	})
	.setHtml("Close");
});