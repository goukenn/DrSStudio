(function(){
	igk.system.createNS("igk.wmi",{
		reloadview: function(){
			var s = ns_igk.ext.call('getbodycontent');		
			$igk(document.body).setHtml(s );
		},
		instanciate: function(){
			var s = ns_igk.ext.call('getInstances');		
			$igk("#instancedv").setHtml(s );
		}
	});
})();