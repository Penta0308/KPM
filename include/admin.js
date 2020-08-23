function cal_byteS(obj, maxbyte) {

	var tmpStr;
	var temp=0;
	var onechar;
	var tcount;
	tcount = 0;
	tmpStr = new String(document.all[obj].value);
	temp = tmpStr.length;

	for (k=0;k<temp;k++)
	{
		onechar = tmpStr.charAt(k);

		if (escape(onechar).length > 4) {
			tcount += 2;
		}
		else if (onechar!='\r') {
			tcount++;
		}
	}

	if(tcount>maxbyte) {
		reserve = tcount-maxbyte;
		alert("영문 " + maxbyte + "자, 조선어 " + maxbyte/2 + "자 이내로 입력하시기 바랍니다.\r\n초과된 부분은 자동으로 삭제됩니다."); 
		nets_checkS(obj, maxbyte);
		return;
	}	
}

function nets_checkS(obj,max) {
	var tmpStr;
	var temp=0;
	var onechar;
	var tcount;
	tcount = 0;

	tmpStr = new String(document.all[obj].value);
	temp = tmpStr.length;

	for(k=0;k<temp;k++)
	{
		onechar = tmpStr.charAt(k);

		if(escape(onechar).length > 4) {
			tcount += 2;
		}
		else if(onechar!='\r') {
			tcount++;
		}
		if(tcount>max) {
			tmpStr = tmpStr.substring(0,k);			
			break;
		}
	}

	document.all[obj].value = tmpStr;
}

function MMPhotoView(page){
	var url='popup_view_Photo.aspx?mmfileID=' + page;
	var features = 'width=400,height=500,top=30,left=300';
    features    += 'diretories=no,location=no,menubar=no,scrollbars=yes,toolbar=no,resizable=no,';
    features    += 'status=no';
    window_handle= window.open(url,'MMView',features);
}

function MMVideoView(page){
	var url='popup_view_video.aspx?mmfileID=' + page;
	var features = 'width=400,height=500,top=30,left=300';
    features    += 'diretories=no,location=no,menubar=no,scrollbars=yes,toolbar=no,resizable=no,';
    features    += 'status=no';
    window_handle= window.open(url,'MMView',features);
}