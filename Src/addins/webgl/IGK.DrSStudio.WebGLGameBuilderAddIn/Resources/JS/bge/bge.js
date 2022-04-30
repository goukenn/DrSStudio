//----------------------------------------------------------------
// BALAFON GAME ENGINE 
//----------------------------------------------------------------

// require igk.js

"use strict";
//define the module
igk.system.module("bge");


(function(){
	
	const __ERROR__ATTRIBNOT_FOUND__ = 0xe001;
	
	//extends array manipulation
	igk.appendProperties(Array.prototype, 
	{
		toFloatArray: function(){
			return new Float32Array(this);
		},
	});
	
	
	function __initIGK(){
		throw "No BalafonJS IGK found";
		
	}
	if (!igk){
		__initIGK();
	}
	
	igk.system.createNS("igk.bge",{
		gameContext:function(){//game context			
			var m = {};
			igk.system.diagnostic.traceinfo(m, new Error(), 1);
			var m_install_dir = m.dir ;
			//console.debug(m);		
			
			igk.html5.drawing.gameContextListener.apply(this);
			_currentGame = this;
			var m_scene =null;
			igk.appendProperties(this, {
				updateSize:function(gl, _w, _h){
					var c = $igk(this.canvas).getParent().select(".scene").first();
					_w = _w || c.getPixel("width");//this.getSceneWidth();// window.innerWidth;
					_h = _h || c.getPixel("height");//this.getSceneHeight()//window.innerHeight;
					
					
					// console.debug(" >>> " +_w);
					// console.debug(" >>> " +_h);
					// console.debug(" >>> " +c.getComputedStyle("width"))
					
					this.canvas.width = _w;
					this.canvas.height = _h;
					gl.viewport(0,0, _w , _h );
					this.raise("updateSize", {gl:gl, w:_w, h:_h});
				},
				getBaseDir:function(){
					return m_install_dir;
				},
				getScene:function(){
					return m_scene || $igk(this.canvas).getParent().select(".scene").first();
				},
			});
			
			var m_assets = new igk.bge.assetManager(this);
			
			igk.defineProperty(this, "assets", {get:function(){ return m_assets; }});
			
			//configuration
			//diseable context menu
			$igk(this.canvas).reg_event("contextmenu", igk.bge.events.stop);
			//disable mouse wheel
			igk.winui.reg_window_event("wheel", function(evt){
				igk.bge.events.stop.apply(this, [evt]);
				// alert("wheel");
			});
			
		}
	});
	
	igk.system.createNS("igk.bge.errors",{});
	
	//define error constants
	igk.bge.errors.VS_NOT_VALID = 0x1001;
	
	
	//global private var
	var _version = 1.0;
	var _rdate = "14/07/16";
	var _author = "C.A.D. BONDJE DOUE";
	var _clGameObj= igk.system.createNS("igk.bge.gameObjects");
	var _matrix;
	var _currentGame; //single game apps
	var mg_currentProgram; //store global current program in used
	
	igk.defineProperty(igk.bge, "version", {get:function(){return _version;}});
	igk.defineProperty(igk.bge, "release", {get:function(){return _rdate;}});
	igk.defineProperty(igk.bge, "author", {get:function(){return _author;}});
	igk.defineProperty(igk.bge, "currentGame", {get:function(){ return _currentGame;}});
	igk.defineProperty(igk.bge, "currentProgram", {get:function(){ return mg_currentProgram;}});
	

	igk.system.createNS("igk.bge.app",{});	
	igk.system.createNS("igk.bge.input",{});
	igk.system.createNS("igk.bge.drawing2D",{});
	
	igk.system.createNS("igk.bge.texture",{
		texture2d:function(gl, img){
			// console.debug("create texture 2D");
			// console.debug(img);
			var m_texture = gl.createTexture();
			igk.appendProperties(this, {
				bind:function(gl){
					this.useIt(gl);
					gl.pixelStorei(gl.UNPACK_FLIP_Y_WEBGL, true);// make texture to flip y axes 
					//gl.pixelStorei(gl.UNPACK_ALIGNMENT,4);
					gl.texImage2D(gl.TEXTURE_2D, 0, gl.RGBA, gl.RGBA, gl.UNSIGNED_BYTE, img);
					// gl.texImage2D(gl.TEXTURE_2D, 0, gl.RGBA,256,256,0, gl.RGBA, gl.UNSIGNED_BYTE, img);
					gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MAG_FILTER, gl.LINEAR );
					gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MIN_FILTER, gl.LINEAR );
					// gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_WRAP_S, gl.REPEAT  );
					// gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_WRAP_T, gl.REPEAT );		

					// non power of 2 texture must be clamp to edge
					gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_WRAP_S, gl.CLAMP_TO_EDGE  );
					gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_WRAP_T, gl.CLAMP_TO_EDGE );						
				},
				useIt:function(gl){
					gl.activeTexture(gl.TEXTURE0);
					gl.bindTexture(gl.TEXTURE_2D, m_texture);
				},
				unloadContent:function(gl){
					gl.deleteTexture(m_texture);
				}
				
			});
			
			igk.defineProperty(this, "id",{get:function(){return m_texture;}, readonly:1});
			this.bind(gl);
		}
	});
	

	// igk.appendProperties(igk.bge.texture.texture2d,{
		// magFilter: {
			// nearest: gl.NEAREST
			// linear: gl.LINEAR
		// }
		// wrapMode:{
			// repeat:  gl.REPEAT ,
			 // gl.CLAMP_TO_EDGE
		// }
	// })
	
	
	function __keyMaps(win){
		var m_mappings = [];
		var m_downkeys=[];
		for(var i = 0; i < 255;i++){
			m_mappings[i]=0;
		}
		igk.appendProperties(this, {
			update:function(){
				return this;
			},
			isKeyDown:function(key){
				
				if (m_mappings[key]){
					return 1;
				}
				return 0;
			},
			isKeyRelease:function(key){
				if (this.isKeyDown(key)){
					m_downkeys[key] = 1;
					return 0;
				}
				if (m_downkeys[key]){
					m_downkeys[key]=0;
					return 1;
				}
				return 0;
			}
		});
		function __downKey(evt){
			if (evt.keyCode){
				m_mappings[evt.keyCode]=1;
			}
		};
		function __upKey(evt){
			if (evt.keyCode){
				m_mappings[evt.keyCode]=0;
			}
		};
		igk.winui.reg_window_event("keydown",__downKey);
		igk.winui.reg_window_event("keyup",__upKey);
	}

	var m_maps = null;
	igk.defineProperty(igk.bge.input,"keyMaps",
	{get:function(){
		if (m_maps ==null)
			m_maps = new __keyMaps(window); 
		return m_maps;
	}}
	);
	
	igk.system.createNS("igk.bge.math",{
		mat4:function(){
			
		}
	});
	
	
	function _copyArray(tab1, tab2){
				for(var i = 0; i< 16;i++){
						tab1[i] = tab2[i];
				}
				return tab1;
			};
			
	var g_math = igk.system.createNS("igk.bge.math", {
		mat4:function(){
			var fm= [];
			var cp =[]; //store state
			
			igk.defineProperty(this, "elements", {get:function(){return fm; }});
			
			igk.appendProperties(this, {
				save:function(){
					var tcp = [];
					this.copy(tcp);
					cp.push(tcp);
				},
				restore:function(){
					if (cp.length>0){
						this.set(cp.pop());
					}
				},
				copy:function(d){ // copy the current array elements to d
					return _copyArray(d, fm);
				},
				set:function(d){// copy the current d to arrary elements
					return _copyArray(fm, d);
				},
				// mult:function(d){
					// var o = [];
					// mat4.multiply(o, this.elements, d);
					// return o;
				// },
				translate:function(dx, dy,dz){
					fm[12]+= dx;
					fm[13]+= dy;
					fm[14]+= dz;
					return this;
				},
				rotate:function(angle, ux, uy, uz){
					var rad = angle * Math.PI/180.0;
					var a = fm;
					var a0 = a[0], a1 = a[1], a2 = a[2], a3 = a[3],						
						s = Math.sin(rad),
						c = Math.cos(rad);
					fm[0] = a0 *  c + a2 * s;
					fm[1] = a1 *  c + a3 * s;
					fm[2] = a0 * -s + a2 * c;
					fm[3] = a1 * -s + a3 * c;
					return this;
				},
				scale:function(ex, ey, ez){
					fm[0]*= ex;
					fm[5]*= ey;
					fm[10]*= ez || 1;
					return this;
				},
				scaleXYZ:function(e){
					fm[0]*= e;
					fm[5]*= e;
					fm[10]*= e;
					return this;
				},
				makeIdentity:function(){
						for(var i = 0; i< 16; i++){
							fm[i]= ((i % 5 )==0)?1:0;
						}
						return this;
				},
				makeLookAt:function(eyex, eyey,eyez, centerx, centery, centerz, upx, upy, upz){
  var x0, x1, x2, y0, y1, y2, z0, z1, z2, len;
	      
	 

	    if (Math.abs(eyex - centerx) < glMatrix.EPSILON &&
	        Math.abs(eyey - centery) < glMatrix.EPSILON &&
	        Math.abs(eyez - centerz) < glMatrix.EPSILON) {
	        return mat4.identity(out);
	    }

	    z0 = eyex - centerx;
	    z1 = eyey - centery;
	    z2 = eyez - centerz;

	    len = 1 / Math.sqrt(z0 * z0 + z1 * z1 + z2 * z2);
	    z0 *= len;
	    z1 *= len;
	    z2 *= len;

	    x0 = upy * z2 - upz * z1;
	    x1 = upz * z0 - upx * z2;
	    x2 = upx * z1 - upy * z0;
	    len = Math.sqrt(x0 * x0 + x1 * x1 + x2 * x2);
	    if (!len) {
	        x0 = 0;
	        x1 = 0;
	        x2 = 0;
	    } else {
	        len = 1 / len;
	        x0 *= len;
	        x1 *= len;
	        x2 *= len;
	    }

	    y0 = z1 * x2 - z2 * x1;
	    y1 = z2 * x0 - z0 * x2;
	    y2 = z0 * x1 - z1 * x0;

	    len = Math.sqrt(y0 * y0 + y1 * y1 + y2 * y2);
	    if (!len) {
	        y0 = 0;
	        y1 = 0;
	        y2 = 0;
	    } else {
	        len = 1 / len;
	        y0 *= len;
	        y1 *= len;
	        y2 *= len;
	    }

	    fm[0] = x0;
	    fm[1] = y0;
	    fm[2] = z0;
	    fm[3] = 0;
	    fm[4] = x1;
	    fm[5] = y1;
	    fm[6] = z1;
	    fm[7] = 0;
	    fm[8] = x2;
	    fm[9] = y2;
	    fm[10] = z2;
	    fm[11] = 0;
	    fm[12] = -(x0 * eyex + x1 * eyey + x2 * eyez);
	    fm[13] = -(y0 * eyex + y1 * eyey + y2 * eyez);
	    fm[14] = -(z0 * eyex + z1 * eyey + z2 * eyez);
	    fm[15] = 1;

					
					return this;
				},
				makeFov:function(fov, aspect, near, far){
					 var h = cot( (fov / 2.0) * (Math.PI/180.0) );
            var w =- aspect * h;
            var t = [
					 w  ,   0  ,    0                    ,                         0,
					0   ,  h   ,   0                    ,                         0,
					0   ,  0   ,   far /(far -near)       ,       1,
					0   ,  0   ,   -near *far /(far-near ) , 0
					];
					//copy 
					for(var i = 0; i < 16; i++){
						fm[i]=t[i];
					}
				},
				makeOrtho:function(lx, mx, ly,my,znear, zfar){
					fm[0]=2/(mx-lx);fm[1]=0;fm[2]=0;fm[3]=0;
					fm[4]=0;fm[5]=2/(my-ly);fm[6]=0;fm[7]=0;
					fm[8]=0;fm[9]=0;fm[10]=2/(zfar-znear);fm[11]=0;
					fm[12]=-(mx+lx)/(mx-lx);fm[13]=-(my+ly)/(my-ly);fm[14]=-(zfar+znear)/(zfar-znear);fm[15]=1;
					
					//this.transpose();
					return this;
				},
				makeFrustum:function(lx, mx, ly,my,znear, zfar){
					var A=(lx+mx)/(mx-lx);
					var B=(ly+my)/(my-ly);
					var C=-(znear+zfar)/(zfar-znear);
					var D=-2*(znear*zfar)/(zfar-znear);
					
					fm[0]=2*(znear)/(mx-lx);fm[1]=0;fm[2]=0;fm[3]=0;
					fm[4]=0;fm[5]=2*(znear)/(my-ly);fm[6]=0;fm[7]=0;
					fm[8]=A;fm[9]=B;fm[10]=C;fm[11]=-1;
					fm[12]=0;fm[13]=0;fm[14]=D;fm[15]=0;
					
					return this;
				},
				transpose:function(){
					var tmp = [];
					//copy
					for(var i = 0; i< 16;i++){
						tmp[i] = fm[i];
					}
					fm[0] = tmp[0];
					fm[1] = tmp[4];
					fm[2] = tmp[8];
					fm[4] = tmp[12];
					
					fm[5] = tmp[1];
					fm[6] = tmp[5];
					fm[7] = tmp[9];
					fm[8] = tmp[13];
					
					
					fm[9] = tmp[2];
					fm[10] = tmp[6];
					fm[11] = tmp[10];
					fm[12] = tmp[14];
					
					
					fm[13] = tmp[3];
					fm[14] = tmp[7];
					fm[15] = tmp[11];
					fm[16] = tmp[15];
					
					return this;
				},
				mult:function(d){
					if (d instanceof igk.bge.math.mat4){
						d = d.elements;
					}
					var g = [];
					g = _copyArray(g, fm);
					var k = 0;
					var s = 0;
					var offset = 0;
						// for(var k = 0; k < 16; k++){
							for(var js = 0; js<4; js++){
								for(var x = 0;x < 4; x++){
									s = 0;
									for(var j = 0;j < 4; j++)
									{
										s += g[offset+j]*d[(j*4)+x];
									}
									fm[k] = s;
									k++;
								}
								offset+=4;
							}
							
						// }
					return this;
				}
			});
		
			this.makeIdentity();
		}
	});
	
	igk.appendProperties(g_math.mat4.prototype, {
		make:function(){
			return new Float32Array(this.elements);
		}
	});

	// var mat = new igk.bge.math.mat4();
	// var mat2 = new igk.bge.math.mat4();
	// console.debug("test mat4 ");
	// console.debug(mat.mult(mat2));
	
	
	igk.appendProperties(igk.bge,{
		exception:function(m,c){//exception object			
			this.message = m;
			this.code = c;
			
			igk.appendProperties(this, {
				toString: function() {
					return c+":"+m;
				}
			});
		}
	});
	igk.system.createNS("igk.bge.math.mat4",{
		createIdentity:function(){
			return new g_math.mat4();
		}
	});
	igk.system.createNS("igk.bge.math.matrix",{
		create:function(){return Float32Array(16); }
	});
	
	_matrix = igk.bge.math.matrix;
	
	igk.system.createNS("igk.bge.shaders",{},{
		desc:"used namespace of shaders"
	});
	
	
	
	
	
(function(){
	var sm_shader = null; //atomic shaders.....
	var sm_counter = 0;   //store number of total program created on this glcontext
	var sm_programs = []; //list of program created
	var m_errors = [];
	var m_ecode=0;
	const __CLASS_NAME__ = "igk.bge.shader.shaderProgram";
	
	var _fc = function (){//singleton shader object
			if (sm_shader){
				// console.debug("single ton instance");
				return sm_shader;
			}
			if (this instanceof igk.object){	
				return  new _fshader();
			}
			// console.debug(this);
			// console.debug(this instanceof igk.object);			
			igk.appendProperties(this, {
				getError:function(){
					return m_errors.join("\n");
				},
				loadAndCompile: function(gl, arraylistVShader, arraylistFShader){//create an compile
					//igk.winui.toast.make("load and compile").show();
					m_errors = [];
					var p = null; //program data
					//return null if failed
					var vshader = [];
					var fshader = [];
					var program = gl.createProgram();
					var vertexShader="";
					var fragmentShader="";
					var v_str = "";
					
					for(var i = 0; i < arraylistVShader.length; i++){
						v_str = arraylistVShader[i];
						if (!v_str){
							m_errors.push("string is empty is not a valid vertex shader");
							m_ecode = igk.bge.errors.VS_NOT_VALID;
							return;							
						}
						vertexShader = gl.createShader(gl.VERTEX_SHADER);
						gl.shaderSource(vertexShader, v_str);
						gl.compileShader(vertexShader);
						
						if (!gl.getShaderParameter(vertexShader, gl.COMPILE_STATUS)){
							console.error("Error when compiling vertext shader ! ", gl.getShaderInfoLog(vertexShader));
							// console.log(v_str);
							// console.log(vertexShader);
							return;
						}
						vshader[i] = {id:vertexShader,src:v_str};
						
						gl.attachShader(program, vertexShader);
					}
					
					for(var i = 0; i < arraylistFShader.length; i++){
						
						v_str = arraylistFShader[i];
						if (!v_str){
							throw new igk.bge.exception(v_str +" is not a valid fragment shader", igk.bge.errors.FS_NOT_VALID);						
						}
						fragmentShader = gl.createShader(gl.FRAGMENT_SHADER);
						gl.shaderSource(fragmentShader, v_str);
						gl.compileShader(fragmentShader);
						if (!gl.getShaderParameter(fragmentShader, gl.COMPILE_STATUS)){
							console.error("Error when compiling fragment shader ! ", gl.getShaderInfoLog(fragmentShader));
							return;
						}
						fshader[i] = {id:fragmentShader,src:v_str};		
						gl.attachShader(program, fragmentShader);
	
					}
	
	
			
			//link program
			gl.linkProgram(program);
			if (!gl.getProgramParameter(program, gl.LINK_STATUS)){
				console.error("Error when compiling program link failed shader ! ", gl.getProgramInfoLog(program));
				return;
			}
			
			gl.validateProgram(program);
			if (!gl.getProgramParameter(program, gl.VALIDATE_STATUS)){
				console.error("Validate program ! ", gl.getProgramInfoLog(program));
				return;
			}
			p =new function(){ //program encapsulation
				var m_gl = 0;
				var m_index = (sm_counter);
				sm_counter++; //
				
				igk.appendProperties(this, {
					
					vertextShaders:vshader,
					fragmentShaders:fshader,
					attributes:{}, //store shader attributes variable location
					uniforms:{}, //store shader uniforms variable location
					buffers:{}, //store the created buffer
					
					toString:function(){
						return __CLASS_NAME__;
					},
					useIt:function(gl){				
						gl.useProgram(this.id);
						if (mg_currentProgram && (mg_currentProgram!=this)){
							mg_currentProgram.m_gl=0;
						}
						m_gl=gl;
						mg_currentProgram= this;
					},
					freeIt:function(){
						if (m_gl){
							m_gl.useProgram(null);
							if (mg_currentProgram && (mg_currentProgram==this)){
								mg_currentProgram = null;							
							}
							m_gl=null;						
						}
						
					},
					setAttribute: function(n, tab, bpe, stride, offset){
						// @n:name of attribute to update
						// @tab: attribute passing
						// @bpe: number of entries per vertex must be 1, 2,3 or 4	
						// @stride : to get the data
						// @offset : to get the 						
						var gl=m_gl;
						offset = offset||0;
						stride = stride ||0;
						var s = gl.getAttribLocation(this.id, n);
						if (s==null)igk.die("no attribute location found {"+n+"}"+s, __ERROR__ATTRIBNOT_FOUND__);
						var buffer = this.buffers[n] || (this.buffers[n] = gl.createBuffer());// buffer=g
						gl.bindBuffer(gl.ARRAY_BUFFER, buffer);
					    gl.bufferData(gl.ARRAY_BUFFER, tab, gl.STATIC_DRAW);	
												
						// console.debug(n);
						gl.vertexAttribPointer(
						s, 
						bpe,//3,//number of elements 
						gl.FLOAT,
						gl.FALSE,
						stride * Float32Array.BYTES_PER_ELEMENT,
						offset * Float32Array.BYTES_PER_ELEMENT ); 						
						gl.enableVertexAttribArray(s);
					},
					setAttribute3f:function(n,x,y,z){
						this.setAttribute(n,
							new Float32Array([x,y,z, x,y,z,x,y,z]),
							3);
					},	
					initAttribLocation:function(gl, tn){//init attrib list
						var k = 0;
						var f = 1;
						for(var i = 0; i < tn.length; i++){
							k= gl.getAttribLocation(this.id, tn[i]);							
							this.attributes[tn[i]] =  k; 
							f = f &&  (k != null);							
						}
						return f;
					},
					initUniform:function(gl, tn){//init uniform list
						var k = 0;
						var f = 1;
						for(var i = 0; i < tn.length; i++){
							k= gl.getUniformLocation(this.id, tn[i]);							
							this.uniforms[tn[i]] = k; 
							f = f && (k != null);
						}
						return f;
					},
					setUniform:function(n, v){//n:name; v:value matrix 4
						return this.setUniformMat4(n,v.make());
					},
					setUniformMat4:function(n,v){
						
						if (!m_gl){
							console.error("you must first set gl with useIt");
							return this;
						}
						var k = 0
						var i= m_gl.getUniformLocation(this.id, n);
						 if (i!=-1){
							 m_gl.uniformMatrix4fv(i,  gl.GL_FALSE, v);
						 }
						 return this;
					},					
					setUniform1f:function(n,v){						
						var i= m_gl.getUniformLocation(this.id, n);
						 if (i!=-1){
							 m_gl.uniform1f(i, v);
						 }
						 return this;
					},
					getUniform: function(n){
						var i= m_gl.getUniformLocation(this.id, n);
						if (i!=-1){
							return m_gl.getUniform(this.id,i);
						}
						return null;
					}
				});
			
			
				igk.defineProperty(this,"id", {get:function(){return program; }});
				igk.defineProperty(this,"index", {get:function(){return m_index; }});
			
			};
		
			
			return p;
			},
				toString:function(){return "igk.bge.shader"; }
			});
			sm_shader = this;
			return sm_shader;
	};
	
	//create a singleton shader manager
	new _fc();	
	igk.defineProperty(igk.bge, "shader", {
		get:function(){
			return sm_shader;
		}
	});
	
	//console.debug("shader "+igk.bge.shader);
	
	})();
	
(function(){
//manage game object
var _sp = 0;//set parent
igk.system.createNS("igk.bge",{
	gameObject: function(){
		var _program = null;//store program used to render this object
		var _parent =null;//store the parent of this game object
		var _transform=null;
		// var _vsh = []; //array of vertex shader
		// var _fsh = []; //array of fragment shader
		igk.appendProperties(this,{
			init:function(){
				
			},
			render:function(gl){	//render game object				
			},
			loadContent : function(gl){
			},
			setProgram:function(p){
				_program=p;
			},
			setParent:function(p){
				if (_sp){
					_parent = p;
				}
			}
		});
		
		igk.defineProperty(this, "program", {
			get: function(){return _program; }
		});
		igk.defineProperty(this, "parent", {
			get: function(){return _parent; }
		});
		igk.defineProperty(this, "transform", {
			get: function(){
				if (!_transform)
					_transform = igk.bge.math.mat4.createIdentity();
				return _transform; //model view transform 
			}
		});
	},	
	gameContainer:function(){//data object that can have on or more children
		igk.bge.gameObject.apply(this);
		var m_clist = new igk.system.collections.list();
		var base =  igk.system.getBindFunctions(this);
		var q =this;
		igk.appendProperties(this,{
			add:function(item){
				if (_sp)return;
				_sp =1;
				m_clist.add(item);
				item.setParent(this);
				_sp=0;
				return q;
			},
			remove:function(item){
				if (_sp)return;
				_sp =1;
				m_clist.add(item);
				item.setParent(null);
				_sp =0;
				return q;
			},
			getchildCount:function(){
				return m_clist.getCount();
			},
			getChilds:function(){
				return m_clist.toArray();
			},
			eachChild:function(callback){
				m_clist.forEach(callback);
				return this;
			},
			loadContent:function(gl){
				base.loadContent(gl);
				this.eachChild(function(item,index){
					item.loadContent(gl);
				})
			},
			render:function(gl){
				this.eachChild(function(item,index){
					item.render(gl);
				});
			}
			
		});
	}
});})();
	
(function(){//manage game object

	var __base_spriteBatch =null;
	igk.system.createNS("igk.bge.gameObjects",{
		create:function(name){
			//create a game object by name
			var c = _clGameObj;
			if((name in c) && (typeof(c[name])=='function') && (name!='create')){
				var r = arguments.length>1? igk.system.array.slice(arguments,1) : 0;
				var t = new c[name]();
				if (r)
					t.init.apply(t, r);
				return t;
			}
			return null;
		},
		spriteBatch:function(game){
			igk.bge.gameContainer.apply(this);
			this.game = game;
			var base = __base_spriteBatch || (__base_spriteBatch=igk.system.getBindFunctions(this));
			var uG = igk.bge.math.mat4.createIdentity();
			var pG = igk.bge.math.mat4.createIdentity();
			var vG = igk.bge.math.mat4.createIdentity();
			// _clGameObj.apply(this);
			igk.appendProperties(this,{
				"bind":function(){
					this.program.useIt();
				},
				setProgram:function(p){	
					//do nothing
				},
				render:function(gl){
					if (!this.program)return;
					this.program.useIt(gl);
					this.eachChild(function(item, index){				
						item.render(gl);
					}); 
					this.program.freeIt(gl);
				},
				loadContent :function(gl){
					//load sprite batch files
					if (igk.bge.shaders.spritebatchVS && igk.bge.shaders.spritebatchFS){
						var p = igk.bge.shader.loadAndCompile(gl,
							[igk.bge.shaders.spritebatchVS],
							[igk.bge.shaders.spritebatchFS]);
						if(!p){
							throw "can't load spritebatch program";
						}
						//set program to root
						base.setProgram.apply(this, [p]);
					}
					base.loadContent(gl);
					// console.debug("program id "+this.program.id.id);
				
					var q= this;
						
					
						//console.d
					function setupUniform(){
							//mg_currentProgram = this.program;
							// console.debug(uG);
							//console.debug(this.program);
							var p = this.program;
							if (!p)
								return;
							var w = this.game.canvas.width;
							var h = this.game.canvas.height;
							p.useIt(gl);
							
							//sample frustum
							//pG.makeIdentity().makeFrustum(-1, 1, -1, 1, 0.4, 10));//
							//vG.makeLookAt(0.0, 0.0, 5.0, 0.0, 0.0, 0.0, 0.0, 1.0, 0.0)
							
							//console.debug(w);
							var up = pG.makeIdentity().makeOrtho(0,w, h,0, -0.1, 100);//.scale(1,-1, 1);							
							p.setUniform("uGlobalView", uG.makeIdentity());
							p.setUniform("uProjection", up);
							p.setUniform("uModelView", vG.makeIdentity().scale(1,1,1));
							p.freeIt();
					};	
					if (this.program){	
					this.game.on("sizeChanged", function(){
						setupUniform.apply(q);
					});
					}
					setupUniform.apply(q);					
					this.eachChild(function(item, index){
						item.loadContent(gl);
					}); 
				}
			});
		},
		container:function(){
			igk.bge.gameContainer.apply(this);
			var base = igk.system.getBindFunctions(this);
			igk.appendProperties(this,{
				render:function(gl){					
					base.render(gl);
				}
			});
		},
		triangle: function(){			
			igk.bge.gameContainer.apply(this);
			var _vertices = [0, 0, 0.5, -0.5, -0.5, -0.5];
			var base = igk.system.getBindFunctions(this);
			var vertexBufferObject =null;
			
			function _bindAttrib(gl,pid, vertexBufferObject, vertices){
				var positionAttribLocation = gl.getAttribLocation(pid, 'inPosition');
						
				gl.bindBuffer(gl.ARRAY_BUFFER, vertexBufferObject);
				gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(vertices), gl.STATIC_DRAW);
				//console.debug(positionAttribLocation);
				gl.vertexAttribPointer(
				positionAttribLocation, 
				2,//number of elements 
				gl.FLOAT,
				gl.FALSE,
				2 * Float32Array.BYTES_PER_ELEMENT,
				0); 
				
				 gl.enableVertexAttribArray(positionAttribLocation);
			}
			//console.debug(base);
			//override 
			igk.appendProperties(this,{
				loadVertices:function (gl, vertices){
					_vertices=vertices;
					if (mg_currentProgram)					
					_bindAttrib(gl,mg_currentProgram.id, vertexBufferObject, vertices);
				},
				render:function(gl){
						
					if (this.program){
						this.program.useIt(gl);
						//console.debug(vertexBufferObject);
						// gl.bindBuffer(gl.ARRAY_BUFFER, vertexBufferObject);
						//console.debug("primal obj");
					}
					else{						
						if (!mg_currentProgram)
							return;
						
						var m =  mg_currentProgram.getUniform("uModelView") ;
						this.transform.save();
						mg_currentProgram.setUniformMat4("uModelView", this.transform.mult(m).elements.toFloatArray());
						_bindAttrib(gl,mg_currentProgram.id, vertexBufferObject, _vertices);
						gl.drawArrays(gl.TRIANGLE_FAN, 0,3);
						this.eachChild(function(item, index){				
							item.render(gl);
						}); 
						
						mg_currentProgram.setUniformMat4("uModelView", m);//restore matrix
						this.transform.restore();
					}
					
					
				}, 
				loadContent :function(gl){
					base.loadContent(gl);
					vertexBufferObject = gl.createBuffer();
					gl.bindBuffer(gl.ARRAY_BUFFER, vertexBufferObject);
					gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(_vertices), gl.STATIC_DRAW);	
					if (this.program){
						//bind to program
						
								var positionAttribLocation = gl.getAttribLocation(this.program.id, 'inPosition');								
								//console.debug(positionAttribLocation);
								gl.vertexAttribPointer(
								positionAttribLocation, 
								2,//number of elements 
								gl.FLOAT,
								gl.FALSE,
								2 * Float32Array.BYTES_PER_ELEMENT,
								0); 
								
								gl.enableVertexAttribArray(positionAttribLocation);
						}
				}
			});
		}
	
		,DagNode:function(host){
			igk.bge.gameObject.apply(this);
			var m_local = igk.bge.math.mat4.createIdentity();
			var m_host = host;
			
			igk.appendProperties(this, {render:function(){
				m_local.bind();
			}});
		}
	
	});})();
	
	igk.system.createNS("igk.bge.events",{
		stop: function(evt){//stop event callback
			evt.stopPropagation();
			evt.preventDefault();
		},
		SIZE_CHANGED:"sizeChanged"
	});
	
	igk.system.createNS("igk.bge.demos",{
		redtriangle:function(){ //basics demos
			var _w = 0;
			var _h = 0;
			var m_program;
			igk.bge.gameContext.apply(this);
			
			var _triangle  = new igk.bge.gameObjects.triangle();
			var _base = {"loadContent":this.loadContent};
			igk.appendProperties(this,{				
				initGame: function(){
					//GameComponent.add(gameFactory.create("spritebatch")new SpriteBatch());
					
				},
				loadContent:function(gl){
					// _triangle.loadShader(
					// ["precision mediump float; attribute vec2 inPosition; varying vec4 inFrag; void main(){ inFrag = vec4(1.0,0,0,1.0) ; gl_Position = vec4(inPosition, 0, 1);  } "],
					// ["precision mediump float; varying vec4 inFrag; void main(){ gl_FragColor = inFrag; }"]);
					_triangle.loadContent(gl);
				},
				render:function render(gl){
					// super(gl);
					// igk.html5.drawing.gameContextListener.render.apply(this,[gl]);
					gl.clearColor(0.2,0.2, 0.2, 1.0);
					gl.clear(gl.COLOR_BUFFER_BIT|gl.DEPTH_BUFFER_BIT);					
					_triangle.render(gl);
				}
			});
	}});
		
})();

(function(){
	
// igk.system.module.load("/load_shader.php");
igk.system.module.load("/igk.bge.input.gamepad.js");
igk.system.module.load("/igk.bge.gameObjects.mesh.js");
igk.system.module.load("/igk.bge.gameObjects.gltf.js");
igk.system.module.load("/igk.bge.assetManager.js");
igk.system.module.load("/igk.bge.drawing2D.graphics.js");
igk.system.module.load("/gl-matrix.js");
})();

(function(){
	//manage game applications
	var v_gameApp=0;
	function __scriptGameContext(fc){		
		igk.bge.gameContext.apply(this);
		var base= igk.system.getBindFunctions(this);
		
		//function _init_invokingList(q,t){ 
		var scr = 
			"var t = ['render', 'updateWorld','unloadContent','loadContent','initGameLogic'];"+
			"var fc=0;"+
			"for(var i=0; i <t.length; i++){"+
			"try{"+
			"if(typeof( fc = eval(t[i])) == 'function') "+
			"this[t[i]] =fc;"+
			"}catch(e){}"+
			"}";
		
		eval( fc+" "+scr);
		v_gameApp=this;
		
	}
	function __bindSrc(q,g){
		//console.debug("init "+q.o.tagName.toLowerCase());
		//igk.eval.apply(q,[g,q]);
		// var c = eval("(function(){"+g+"; return this; })" );
		
		
		// console.debug(c);//.test();
		var dv = q.getParent().add('div');
		dv.addClass("igk-bge-surface script");		
		//passing a function is required because CreateContext will create the properties listener
		igk.html5.drawing.CreateContext(dv.add("canvas"), function (){			
			return __scriptGameContext.apply(this, [g]);
		});
	}
	function __initGameApp(){
		if (v_gameApp)
			throw ("no multi game allowed ");
		if (this.o.tagName.toLowerCase() != "script")
			throw ("canvas is required for balafon/igk-winui-bge-script ? "+this.o.tagName.toLowerCase());
		
		var g = this.getAttribute("src");
		if (!g){
			g = this.getText();
			__bindSrc(this, g);
		}
		else{
			throw ("not implement");
		}
		
	}
	
	
	igk.winui.initClassControl("igk-winui-bge-script", __initGameApp);
	
	
})();