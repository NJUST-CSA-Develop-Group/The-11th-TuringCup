#pragma strict


var state:int=0;
var changeCD:float=0;
var justSwitched:boolean=false;
private var rnd:float;


function Start () {

}

function Update () {

if(changeCD>0) changeCD-=Time.deltaTime;
if(changeCD<0) changeCD=0;


//*****************changing states
if(changeCD==0&&justSwitched==false)
{
rnd=Random.Range(1, 100);

	if(state==0&&justSwitched==false)	//idle
	{
		if(rnd<10) state=1;
		if(rnd>10&&rnd<20) state=2;
		if(rnd>20&&rnd<30) state=3;
		if(rnd>30) state=0;
		justSwitched=true;
	}

	if(state==1&&justSwitched==false) //idleLook
	{ 
	justSwitched=true;
	state=0;
	}
	
	if(state==2&&justSwitched==false) //Yes
	{
	justSwitched=true;
	state=0;
	}

	if(state==3&&justSwitched==false) //No
	{
	justSwitched=true;
	state=0;
	}






}


//*****************end changing states






//******************doing whatever states do


if(state==0)	//idle
{
	
	if (changeCD==0) changeCD=30.0/30.0;

	if(!GetComponent.<Animation>().IsPlaying("idle")){
	GetComponent.<Animation>().CrossFade("idle");
	}

	
}


if(state==1)	//look
{
if (changeCD==0) changeCD=60.0/30.0;
	if(!GetComponent.<Animation>().IsPlaying("idle_look")){
	GetComponent.<Animation>().CrossFade("idle_look");

	}

	
}

if(state==2)	//yes
{
if (changeCD==0) changeCD=38.0/30.0;

	if(!GetComponent.<Animation>().IsPlaying("happy")){
	GetComponent.<Animation>().CrossFade("happy");

	}

	
}

if(state==3)	//yes
{
if (changeCD==0) changeCD=38.0/30.0;

	if(!GetComponent.<Animation>().IsPlaying("sad")){
	GetComponent.<Animation>().CrossFade("sad");

	}

	
}





justSwitched=false;

//********************end states part


}