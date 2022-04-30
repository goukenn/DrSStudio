//
//css_builder_mains.js
//
(function(){

//declaring variable
var q = $igk(igk.getParentScript());
function _get_style_name(n){

	var m = "";
	for(var i = 0; i <n.length; i++)
	{
		var t =n.charCodeAt(i);
		
		if ((t>=65) && (t<= (65+26)))
			m += '-';
		m += n[i].toLowerCase();
	}
	
	return m;

}


igk.ready(function(){
var s = $igk("#preview").getItemAt(0);

if (!s)
	return;
var m_styles = {};
var m_reloading = false;
var m_keys = null;
var m_tab = null;

function __click_func(evt){
	evt.preventDefault();
	var s = this.innerHTML;	
	if (igk.system.string.endWith(s.toLowerCase(), 'color'))
	{
		igk.ext.call("pickColor", s);
	}
};
function __valkey(evt){
	if (evt.keyCode ==13)
	{
		s.o.style[$igk(this).getAttribute("cssproperty")] = this.value;
		reloadStyle();
	}
}
function __loadProp(pattern){
	
	q.select('*').each(function(){
		this.unregister();
		return true;
	});
	//unregiter all
	q.setHtml("");
	if (pattern == "")
		pattern == null;
	var b = m_keys;
	var v_tab = m_tab;
	for(var i = 0; i< b.length; i++) //for (var i in v_tab){
	{
		if ((pattern !=null) && ( b[i].indexOf(pattern) == -1))
		{
			continue;
		}
		k = v_tab[b[i]];
		m = igk.createNode("li");
		m.setCss({paddingLeft:"10px", paddingRight:"10px"});
		m.add("a")
		.addClass("dispb")
		.setAttribute("href", "#")
		.reg_event("click", __click_func)
		.setHtml(""+k);
		
		var v  = s.o.style[k];
		if (v == "undefined")
			v = null;
		var input = m.add("input");
		
		
		m_styles[k] = input;
		
		input.addClass("igk-form-control dispb")
		.setAttribute("type", "text")
		.setAttribute("value", v)
		.setAttribute("cssproperty", k)
		.reg_event("keyup", __valkey)
		.reg_event("change", (function(k){
			var m_k = k;
			return function(evt){
				if (m_reloading)
					return;
			try{
				s.o.style[k] = this.value;
				reloadStyle();
			}
			catch(ex){
				igk.show_notify_error("Exception : ", k +" <br />"+ex);
			}
			}
		})(k))
		;
		//t+= m.o.outerHTML;
		q.appendChild(m);
	}
}
function reloadStyle()
{
	if (m_reloading)
		return;
	m_reloading = true;
	
	for(var i in m_styles)
	{
		var v  = s.o.style[i]+'';
		if (v == "undefined"){
		
			v = null;
		}
		m_styles[i].o.value =  v;
		
	}
	m_reloading = false;
}
function getCssList(){
	var t = "";
	var v_tab = igk.css.getProperties();
	var m =null;
	var k = null;

	
	var b = [];
	for (var i in v_tab){
		if (i == "csstext")
			continue;
		b.push(i);
	}
	igk.system.array.sort(b);
	m_keys = b;
	m_tab = v_tab;
	
	__loadProp(null);
	
	//q.setHtml(t);
};
getCssList();

igk.system.createNS("cssbuilder", {
	searchProperty: function(pattern){
		__loadProp(pattern);
	},
	setvalue: function(n, v){
		s.o.style[n] = v;
		reloadStyle();
	},
	getList: function(){
		return m_styles;
	},
	getStyleData: function(){		
	
		var r = s.o.style["cssText"];
		if (r)
			return r;
		var m = "";
		for(var i in m_styles)
		{
			var v = s.o.style[i]+'';
			if ((v == 'undefined') || (v ==''))
				continue;
			//get style value
			 var k = _get_style_name(i);
			
			m += k + ":" + v + ";";
		}
		return m;
	}
});
});


})();