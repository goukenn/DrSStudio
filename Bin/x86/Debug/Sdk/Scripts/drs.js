(function(){
window.winex = window.external;

igk.system.createNS("igk.ctrl.selectionmanagement", {
	disable_selection: function(target)
	{
		if (target == null)
			return;
		var t = $igk(target).o;

		if (typeof(t.onselectstart) != "undefined")
			t.onselectstart=function(){ return false;}; 
		if (typeof(t.style.MozUserSelect) != "undefined")
			t.style.MozUserSelect = "none";
		
		t.onblur=function(){ return false;};
		t.ondragstart=function(){ return false};	
		t.style.cursor = "default";
	},	
	initanimover : function(){
		//register anim over 
		$igk(document.body).select(":igk-js-anim-over").each(function(){
			var q = this;
			var source = this.getAttribute("igk-js-anim-over");
			var store = {};
			var t = eval("new Array("+source+")");
			for(var m in t[0])
			{
				store[m] = q.getComputedStyle(m);
			}
			this.reg_event("mouseover", function(){  eval( "q.animate("+source+");");});
			this.reg_event("mouseleave", function(){ q.animate(store, t[1]);});
			return false;
		});
	},
	initnode: function(){
		var q = this;
		var source = this.getAttribute("igk-js-anim-over");
		var store = {};
		if (source){
		var t = eval("new Array("+source+")");
		for(var m in t[0])
		{
			store[m] = q.getComputedStyle(m);
		}
		this.reg_event("mouseover", function(){  eval( "q.animate("+source+");");});
		this.reg_event("mouseleave", function(){ q.animate(store, t[1]);});
		}
	}
});
    
igk.ctrl.bindAttribManager("igk-node-disable-selection", function(){	
	var s =  igk.system.convert.parseToBool(this.getAttribute("igk-node-disable-selection"));		
	if (s == true)
	{				
		var q = this;
		igk.ctrl.selectionmanagement.disable_selection(q);
		q.select("*").each(function(){
		igk.ctrl.selectionmanagement.disable_selection(q.o);
		});
	}
});

})();