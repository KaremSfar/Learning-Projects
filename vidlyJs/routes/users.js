const express = require('express');
const router = express.Router();
const mongoos = require('mongoose');
const {User,validate} = require('../models/User');
const _ = require('lodash');
const bcrypt = require('bcrypt');
const jwt = require('jsonwebtoken');
const auth = require('../middleware/auth');


router.get('/me',auth,async (req,res)=>{
    const user = await User.findById(req.user._id).select("-password");
    res.send(user);
});

router.get('/',auth,async (req,res)=>{
    const users = await User.find();
    res.send(users)
});

router.post('/',async (req,res)=>{
    const {error} = validate(req.body);
    if(error){
        res.status(400).send(error.details[0].message);
        return;
    }

    let user = await User.findOne({email:req.body.email});
    if(user){
        res.status(400).send('User registered');
        return;
    }
    user = new User(_.pick(req.body,['name','email','password']));

    const salt = await bcrypt.genSalt(10);
    user.password = await bcrypt.hash(user.password,salt);

    user = await user.save();

    const token = user.generateAuthToken();
    res.header('x-auth-token',token).send(_.pick(user,['id','name','email']));
});

module.exports = router;