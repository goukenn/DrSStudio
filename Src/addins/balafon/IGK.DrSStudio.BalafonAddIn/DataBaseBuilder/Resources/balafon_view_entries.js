//balafon view entries scripts
//Author : C.A.D BONDJE DOUE
(function(q){

var t = $igk(q);

function editrow(item){
var r = $igk(item);
var rid = $igk(item).getAttribute("clRow");


//start row edition
var tr = igk.createNode("tr");
tr.add("td").addSpace();
var s = r.select("td");
var td = null;
var m_rs = s;

for(var i = 1; i < s.getCount() -2; i++)
{
	var e = $igk(s.getItemAt(i));
	td = tr.add("td");
	td.add("input")
	.setAttribute("class", "igk-form-control")
	.setAttribute("type", "text")
	.setAttribute("id", e.getAttribute("cl"))
	.setAttribute("value", e.getHtml());
	
}

td = tr.add("td").setAttribute("colspan",2);
var input = td.add("input")
	.setAttribute("class", "igk-form-control igk-btn igk-btn-default")
	.setAttribute("type", "button")
	.setAttribute("value", "Update")
	.setAttribute("onclick", "javascript: ns_igk.ext.call('updateEntry', this.getdata());  this.restore(); return false;");
	
input.o.restore=function(){
	tr.o.parentNode.replaceChild(r.o, tr.o);
};
input.o.getdata = function(){
var t = {};
var s = tr.select("input");
for(var i = 0; i < s.getCount() -1; i++)
{
var e = $igk(s.getItemAt(i));

if (e.o.type == "text"){
	t[e.getAttribute("id")]=e.o.value;			
	$igk(m_rs.getItemAt(i + 1)).setHtml(e.o.value);
}
}	
	t["rid"] = rid;
	//igk.show_prop(t);
	
	var ms = ns_igk.JSON.convertToString(t);
	//alert("converto string : " + ms);
	return ms;
};
//replace node
r.o.parentNode.replaceChild(tr.o, r.o);
}
igk.system.createNS("igk.balafon",{
	editrow: editrow
});
    igk.system.createNS("igk.R", {
        __: function (n) {
            return ns_igk.ext.call('lang', n);
        }
    });
    // igk.show_notify_prop(igk.R);
    //t.add("div").setHtml("Resource : ");
})(ns_igk.getParentScript());