<!--Cloak 
	// Center Content In Browser Window
	netscape = navigator.appName == "Netscape" ? true : false;
	function setPositions() {
		winW = (netscape)? window.innerWidth-16 : document.body.offsetWidth-20;
		winH = (netscape)? window.innerHeight : document.body.offsetHeight;
		contentLeft = (winW-784)/2 + 0;
		if (winW < 700) {
			contentLeft = 0;
		}
		if (document.getElementById	&& netscape) {
			document.getElementById('content').style.left = contentLeft+"px"	
		}
		if(netscape) {
			document.layers['content'].left = contentLeft;
		}
		else {
			document.all['content'].style.left = contentLeft+"px";		
		}
	}

	// Netscape Resize Fix
	if (netscape) {
		widthCheck = window.innerWidth
		heightCheck = window.innerHeight
		window.onResize = resizeFix;
	} else {
		window.onresize = setPositions;
	}
	function resizeFix() {
		if (widthCheck != window.innerWidth || heightCheck != window.innerHeight)
		document.location.href = document.location.href
	}
//--> end Cloak