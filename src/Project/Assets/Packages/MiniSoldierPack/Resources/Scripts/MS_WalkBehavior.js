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

	if(state==0&&justSwitched==false)	//walk
	{
		if(rnd<10) state=1;
		if(rnd>10&&rnd<20) state=2;
		if(rnd>20&&rnd<30) state=3;
		if(rnd>30) state=0;
		justSwitched=true;
	}

	if(state==1&&justSwitched==false) //left
	{ 
	justSwitched=true;
	state=0;
	}
	
	if(state==2&&justSwitched==false) //right
	{
	justSwitched=true;
	state=0;
	}


	if(state==3&&justSwitched==false) //kneelaim
	{
	justSwitched=true;
	rnd=Random.Range(1, 100);
	if(rnd<50) state=4;
	if(rnd>50&&rnd<100) state=5;
	}


	if(state==4&&justSwitched==false) //kneelsingleshoot
	{
	justSwitched=true;
	state=0;
	}

	if(state==5&&justSwitched==false) //kneelmultishoot
	{
	justSwitched=true;
	state=0;
	}




}


//*****************end changing states






//******************doing whatever states do


if(state==0)	
{
	
	if (changeCD==0) changeCD=30/30.0;

	if(!GetComponent.<Animation>().IsPlaying("moveForward")){
	GetComponent.<Animation>().CrossFade("moveForward");
	}

	transform.Translate(0, 0, 2*Time.deltaTime);
}


if(state==1)	
{
if (changeCD==0) changeCD=20.0/30.0;
	if(!GetComponent.<Animation>().IsPlaying("strafeRight")){
	GetComponent.<Animation>().CrossFade("strafeRight");

	}

	transform.Translate(1*Time.deltaTime, 0, 0);
}

if(state==2)	//jumping
{
if (changeCD==0) changeCD=20.0/30.0;

	if(!GetComponent.<Animation>().IsPlaying("strafeLeft")){
	GetComponent.<Animation>().CrossFade("strafeLeft");

	}

	transform.Translate(-1*Time.deltaTime, 0, 0);
}


if(state==3)	//aim
{
if (changeCD==0) changeCD=30.0/30.0;

	if(!GetComponent.<Animation>().IsPlaying("kneelAim")){
	GetComponent.<Animation>().CrossFade("kneelAim");
	}
}

if(state==4)	//single standing shot
{
if (changeCD==0) 
{
changeCD=10.0/30.0;
var muzzle:Transform = transform.Find("Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 Neck/Bip001 R Clavicle/Bip001 R UpperArm/Bip001 R Forearm/Bip001 R Hand/gunHolder/weapon/Muzzle");
var effect = Resources.Load("SniperFire");
Instantiate (effect, muzzle.position, muzzle.rotation);
//Instantiate (effect, transform.position, transform.rotation);
}

	if(!GetComponent.<Animation>().IsPlaying("kneelSingleShot")){
	GetComponent.<Animation>().CrossFade("kneelSingleShot");
	}
}

if(state==5)	//multi standing shot
{
if (changeCD==0) 
{
changeCD=12.0/30.0;

var muzzle2:Transform = transform.Find("Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 Neck/Bip001 R Clavicle/Bip001 R UpperArm/Bip001 R Forearm/Bip001 R Hand/gunHolder/weapon/Muzzle");
var effect2 = Resources.Load("TripleFire");
Instantiate (effect2, muzzle2.position, muzzle2.rotation);

}

	if(!GetComponent.<Animation>().IsPlaying("kneelMultiShot")){
	GetComponent.<Animation>().CrossFade("kneelMultiShot");
	}
}



justSwitched=false;

//********************end states part


}