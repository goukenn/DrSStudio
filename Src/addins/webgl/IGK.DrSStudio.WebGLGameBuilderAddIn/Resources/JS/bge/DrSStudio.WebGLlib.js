(function(){
"use strict";

var _clGameObj = igk.bge.gameObjects;
var __basefc__ = null;
var __logName = "DrSStudio.WebGLLib.js";

//init external utility classe
(function(){	
	var sys_cons = window.console;	
	var _ext  = window.external;
	if (_ext){
		function _init(){
		var t = {};
		function _send(t, n){
            var m = "[" + __logName+"]-"+n;
				if (sys_cons){
					sys_cons[t](m);
				}				
				_ext.Log(t, m);
		};
		igk.appendProperties(t, {
			debug:function(n){
				_send('debug', n);			
			}
			,error:function(n){_send('error', n);}
			,warn:function(n){_send('warn', n);}
			,log:function(n){_send('log',n);}			
		});
		return t;
		};
	
		window.console = _init();
	}
})();

igk.system.createNS("DrSStudio.WebGLGameBuilder",{
Debug: function(){//debug functions
		igk.bge.gameContext.apply(this);
		var base= __basefc__|| (__basefc__=igk.system.getBindFunctions(this));
		
        var _spriteBatch = null;
        var _triangle = new _clGameObj.triangle();
        var _gamePadsInput = null;

        this.setBgColor({ r: 1, g: 0, b: 0, a: 1 });

        var _x = 0;

        igk.appendProperties(this, {
            loadContent: function (gl) {
                _spriteBatch = new _clGameObj.spriteBatch(this);//.create("spriteBatch", this);

                _spriteBatch.add(_triangle);

                // var p1 = igk.bge.shader.loadAndCompile(gl, 
                // ["precision mediump float; attribute vec2 inPosition; varying vec4 inFrag; void main(){ inFrag = vec4(1.0,1.0, 0,1.0) ; gl_Position = vec4(inPosition, 0, 1);  } "],
                // ["precision mediump float; varying vec4 inFrag; void main(){ gl_FragColor = inFrag; }"]);

                _triangle.setProgram(null);


                _spriteBatch.loadContent(gl);
                //_gamePadsInput = igk.html5.input.gamePads();
                // _spriteBatch.setProgram(p1);
            },
            updateWorld: function (gl) {

                // _gamePadsInput.update();
                // var pOne = _gamePadsInput.playerOne;

                // if (pOne){
                // if (pOne.isKeyPressed(_ns_gameBtn.A)){
                // //console.debug("button A pressed");
                // _x = (_x+10) % 1200;
                // }
                // }

                _triangle.loadVertices(gl, [500, 200, 100, 50, _x, 200]);
                if (_spriteBatch.program) {
                    _spriteBatch.program.useIt(gl);
                    _spriteBatch.program.setAttribute("inColor", [1.0, 0.0, 0.0, 1.0, 1.0, 0.0, 0.0, 0.0, 1].toFloatArray(), 3);
                }



                //console.debug(_gamePadsInput);
            },
            render: function (gl) {
                base.render.apply(this, [gl]);
                //override render
                //_triangle.render(gl);
                _spriteBatch.render(gl);
                // _triangle.loadVertices(gl, [0, 0, 100.5, -100.5, -100.5, -100.5]);
                //_triangle.render(gl);

                //console.debug(gl.getInteger(gl.CURRENT_PROGRAM));
            }
        });


        //igk.appenProperties(this, {
        //    render: function () {
        //        base.render();

        //    }
        //});
}
});


DrSStudio.WebGLGameBuilder.setDebugModeName = function (n) {
    __logName = n;
    }

})();