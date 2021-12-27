# Natural Modelling

Check out the repository's documentation [here](https://bill-richards.github.io/natural-modelling/)

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
