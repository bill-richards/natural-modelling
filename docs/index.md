# Welcome to GSDC's Natural Modelling

This repository groups together some projects based around _Neural Networks_, _Genetic Algorithms_, _Natural Language Processing_ and whatever else I feel fits into the category of _**Natural Modelling**_: that is, computationally modelling concepts, theories, and objects from the world around us.


The basic premise is that all included sub-projects are concerned with modelling naturally ocurring phenomena, and mostly these projects serve little more than to satisfy academic curiosity.

Whenever commits are made to the repository, GitHub Pages willl run [Jekyll](https://jekyllrb.com/) to rebuild the pages in this site, from the content in the Markdown files, and therefore this documentation should always be up to date.

## Getting the source

Because some of the solutions in this repository have dependencies on shared libraries, those libraries (which can be) are included as submodules. 
When you get the source using

```cmd
> git clone https://github.com/bill-richards/natural-modelling.git
```

be sure to also call

```cmd
> git submodules update --init --recursive
```

## Updating the source

### Update all submodules

When you need to update all of the submodules (to be point to the current HEAD of their repositories), run

```cmd
> git submodule update --remote --merge
```

### Update individual submodules

When you want to update an individual submodule (and not ALL submodules) run the following

```cmd
> git submodule update <path-to-submodule>
```

so, for example, to update **only** the _gsdc-common_ submodule within the _evolution_ folder

```cmd
> git submodule update evolution/libraries/gsdc-common
```