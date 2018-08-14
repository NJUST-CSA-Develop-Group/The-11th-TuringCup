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

	if(state==0&&justSwitched==false)	//run
	{
		if(rnd<10) state=1;
		if(rnd>10&&rnd<20) state=2;
		if(rnd>20&&rnd<30) state=3;
		if(rnd>30) state=0;
		justSwitched=true;
	}

	if(state==1&&justSwitched==false) //roll
	{ 
	justSwitched=true;
	state=0;
	}
	
	if(state==2&&justSwitched==false) //jump
	{
	justSwitched=true;
	state=0;
	}


	if(state==3&&justSwitched==false) //aim
	{
	justSwitched=true;
	rnd=Random.Range(1, 100);
	if(rnd<50) state=4;
	if(rnd>50&&rnd<100) state=5;
	}


	if(state==4&&justSwitched==false) //singleshoot
	{
	justSwitched=true;
	state=0;
	}

	if(state==5&&justSwitched==false) //multishoot
	{
	justSwitched=true;
	state=0;
	}




}


//*****************end changing states






//******************doing whatever states do


if(state==0)	//running
{
	
	if (changeCD==0) changeCD=18.0/30.0;

	if(!GetComponent.<Animation>().IsPlaying("run")){
	GetComponent.<Animation>().CrossFade("run");
	}

	transform.Translate(0, 0, 6*Time.deltaTime);
}


if(state==1)	//rolling
{
if (changeCD==0) changeCD=26.0/30.0;
	if(!GetComponent.<Animation>().IsPlaying("roll")){
	GetComponent.<Animation>().CrossFade("roll");

	}

	transform.Translate(0, 0, 6*Time.deltaTime);
}

if(state==2)	//jumping
{
if (changeCD==0) changeCD=28.0/30.0;

	if(!GetComponent.<Animation>().IsPlaying("jump")){
	GetComponent.<Animation>().CrossFade("jump");

	}

	transform.Translate(0, 0, 6*Time.deltaTime);
}


if(state==3)	//aim
{
if (changeCD==0) changeCD=30.0/30.0;

	if(!GetComponent.<Animation>().IsPlaying("aim")){
	GetComponent.<Animation>().CrossFade("aim");
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

	if(!GetComponent.<Animation>().IsPlaying("singleShot")){
	GetComponent.<Animation>().CrossFade("singleShot");
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

	if(!GetComponent.<Animation>().IsPlaying("multiShot")){
	GetComponent.<Animation>().CrossFade("multiShot");
	}
}



justSwitched=false;

//********************end states part


}