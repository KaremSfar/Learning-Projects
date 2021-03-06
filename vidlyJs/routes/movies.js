const express = require('express');
const router = express.Router();
const {Movie,validate} = require('../models/Movie');
const {Genre} = require('../models/Genre');
const auth = require('../middleware/auth');

router.get('/',async (req,res)=>{
    const movies = await Movie.find();
    res.send(movies);
});

router.get('/:id',async (req,res)=>{
    const movie = await Movie.findById(req.params.id)
    if(!movie){
        res.status(404).send('Not found frr');
        return;
    }
    res.send(movie);
});

router.post('/',auth, async (req, res) => {
    const { error } = validate(req.body);
    if (error) return res.status(400).send(error.details[0].message);

    const genre = await Genre.findById(req.body.genreId);
    if (!genre) return res.status(400).send('Invalid genre.');

    let movie = new Movie({
        title: req.body.title,
        numberInStock: req.body.numberInStock,
        dailyRentalRate: req.body.dailyRentalRate,
        genre: {
            _id: genre._id,
            label: genre.label
        }
    });

    movie = await movie.save();
    res.send(movie);
});

router.put('/:id',auth,async (req,res)=>{
    const { error } = validate(req.body);
    if (error) return res.status(400).send(error.details[0].message);

    const genre = await Genre.findById(req.body.genreId);
    if (!genre) return res.status(400).send('Invalid genre.');

    const movie = await Movie.findByIdAndUpdate(req.params.id,
        {
            title: req.body.title,
            genre: {
                _id: genre._id,
                label: genre.label
            },
            numberInStock: req.body.numberInStock,
            dailyRentalRate: req.body.dailyRentalRate
        }, { new: true });

    if (!movie) return res.status(404).send('The movie with the given ID was not found.');

    res.send(movie);
});

router.delete('/:id',auth,async (req,res)=>{
    const movie = await Movie.findByIdAndDelete(req.params.id)
    res.send(movie);
});
module.exports = router;