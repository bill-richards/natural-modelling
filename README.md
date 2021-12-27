# Natural Modelling


This repository groups together some projects based around _Neural Networks_, _Genetic Algorithms_, _Natural Language Processing_ and whatever else I feel fits into the category of **Natural Modelling**


The basic premise is that all included sub-projects are concerned with modelling naturally ocurring phenomena, and mostly these projects serve little more than academic curiosity.


# Getting the source

Because some of the solutions in this repository have dependencies on other libraries, those libraries (which can be) are included as submodules. When you get the source, be sure to also call the following

```
$ git submodule update --init --recursive
```

When you need to update the submodules (to be certain that they are pointing to the current HEAD, run

```
$ git submodule update --remote --meerge
```