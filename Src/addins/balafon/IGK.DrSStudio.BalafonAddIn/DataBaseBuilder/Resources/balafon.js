"use strict";
(function(){
var m_new_entries;
igk.system.createNS("igk.balafon", {
	select_valuePost: function(i){
		if (i.value == '::custom'){
				var c = window.external.getCustomTableLink();			 
				if (c==null)
					return;  			
				$igk(i).add('option').setAttribute('value',c).setHtml(c);
				i.value = c; 
			} 
			ns_igk.ext.call('storeColumn',ns_igk.form.form_tojson(i.form)); 

	},
	reloadview: function(){
		// igk.alert('reload view');
		$igk(document.body).setHtml(ns_igk.invoke('getBodyView'));
		igk.ajx.fn.initnode(document.body);
		// igk.ready();
	},
	setNewEntryKey: function(n){
		m_new_entries = n;
	},
	insertNewEntry: function(t){
	
		
		var s = ns_igk.ext.call('insertNewEntry');		
	
		if (s && m_new_entries){
		    var tab = $igk($igk(t).getParentForm()).select('table').first();
		    
			if (tab){
			    var tr = tab.select('tr.dispn').first();

				if (tr) {
				    tr.rmClass('dispn');
					
					var c = igk.createNode("tr");
					c.setHtml(tr.getHtml());
				    //var c = tr.clone();
					c.addClass('dispn');
					
				
					
				    tr.setAttribute('clRow', m_new_entries);
				    var br = tr.select('.bal-chb');
				    br.setAttribute('value', m_new_entries);
				    
				    tab.o.appendChild(c.o);
				} else {
					alert("no hidden data");
				}
			}
		}
	}

});
})();

(function () {
    igk.ready(
        function () {
            $igk(".clcolumnmemberindex").each(function () {
               // alert("start ok :: ");
                this.addClass("igk-input-regex");
                this.setAttribute("igk:char-regex", "[0-9\-]");
                this.setAttribute("igk:input-regex", "([0-9]+)(-[0-9]+)*");
                igk.winui.initClassObj(this);
                //alert("done == "+this.o.outerHTML);
                return true;
            });
        });
})();