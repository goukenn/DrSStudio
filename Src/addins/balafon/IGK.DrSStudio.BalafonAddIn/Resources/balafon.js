(function(){
  var q = igk.getParentScriptByTagName("form");

  if (!q)
	return;
  
	q.project = new function(){
	  //private object
	  //public project
	  igk.appendProperties(this, {
	  clName: "",
	  clOutputFolder:"",
	  clPlatformTarget:"",
	  clPrefix:'',
	  clDefaultNS: '',
	  clAppTitle:'',
	  clOverrideExisting:'',
	  toString:function(){
			return "igk.android.project";
		},
	  toJSon: function(){
			return igk.JSON.convertToString(this);
		}
	});  
	};
	
	q.getproperty = function(name, defaultv){
		if (q.project[name])
			return q.project[name];
		else
			return defaultv;
	};
	
	
  function update_tab(s){
  var t = igk.ext.call('navigate',s);
  $igk("#menu-tab-content").setHtml(t, true);
  };
  

  
  q.create_project = function(){
  //base setting properties
  for(var i in this.project)
  {
    if (typeof(q[i])!= 'undefined' && (q[i].type!='undefined'))
    {
		switch(q[i].type)
		{
			case "checkbox":
			case "radio":
				if (q[i].checked)
					this.project[i]	= q[i].value;
				else 
					this.project[i] = '';
				break;
			default:			
				this.project[i]	= q[i].value;
				break;
		}
    }
  }

  var s = this.project.toJSon();
  igk.ext.call('createproject', s);
  
  };
  
  q.load_properties = function(n, s){
	if (this.project)
		this.project[n] = s;
  };
	
	
  igk.ready(function (){
  update_tab("application");  
  $igk(q.parentNode).setCss({
  "marginBottom":$igk("#tab_button").getHeight()+2+"px",
  "paddingTop":$igk("#dialog_title").getHeight()+"px",
  });
  });
  


  igk.ctrl.registerAttribManager("menu-target", {desc:"register android menu targets"});
  igk.ctrl.bindAttribManager("menu-target", function(){
  var s =  this.getAttribute("menu-target");

  this.o.onclick = function(){
  update_tab(s);
  return false;
  };
  });
  })();
