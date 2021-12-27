# Getting the source

Because some of the solutions in this repository have dependencies on shared libraries, those libraries (which can be) are included as submodules. 
When you get the source using

```cmd
git clone https://github.com/bill-richards/natural-modelling.git
```

be sure to also call

```cmd
git submodules update --init --recursive
```

## Updating the source

It is important to note that a _**submodule**_ is linked to a repository at a specific version

If the submodules do not refelect the current state of the super repositories -because commits have been made to those super repositories- then you will most likely want to update the submodules to incorporate those changes.

### Update all submodules

When you need to update all of the submodules (to be point to the current HEAD of their repositories), run

```cmd
git submodule update --remote --merge
```

### Update individual submodules

When you want to update an individual submodule (and not ALL submodules) run the following

```cmd
git submodule update <path-to-submodule>
```

so, for example, to update **only** the _gsdc-common_ submodule within the _evolution_ folder

```cmd
git submodule update evolution/libraries/gsdc-common
```
