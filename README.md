# RASP (Reconstruct Ancestral State in Phylogenies) 

With the continual progress of sequencing techniques, genome-scale data are increasingly used in phylogenetic studies. With more data from throughout the genome, the relationship between genes and different kinds of characters is receiving more attention. Here, we present version 4 of RASP, a software to reconstruct ancestral states through phylogenetic trees. RASP can apply generalized statistical ancestral reconstruction methods to phylogenies, explore the phylogenetic signal of characters to particular trees, calculate distances between trees, and cluster trees into groups.

## Install
You could compile RASP from source code or download the compiled RASP achive from:  
- http://mnh.scu.edu.cn/soft/blog/RASP/index.html  
- https://sourceforge.net/projects/rasp2/  

## Tutorials:
- [Tutorial 1. Getting started with RASP](http://mnh.scu.edu.cn/soft/blog/RASP/Tutorial%201.html)  
- [Tutorial 2. Selecting the biogeographic model](http://mnh.scu.edu.cn/soft/blog/RASP/Tutorial%202.html)  
- [Tutorial 3. Test phylogenetic signal](http://mnh.scu.edu.cn/soft/blog/RASP/Tutorial%203.html)  
- [Tutorial 4. Trait evolution](http://mnh.scu.edu.cn/soft/blog/RASP/Tutorial%204.html)  
- [Tutorial 5. Compare trees derived from different genes](http://mnh.scu.edu.cn/soft/blog/RASP/Tutorial%205.html)  
- [Tutorial 6. Modify trees and states](http://mnh.scu.edu.cn/soft/blog/RASP/Tutorial%206.html)  

## Cites
**To maintain RASP, support users and secure fundings, it is important for us that you cite our paper:**  

Program:
```
Yu Y, Blair C, He XJ. 2019. RASP (Reconstruct Ancestral State in Phylogenies): a tool for historical biogeography. Molecular Biology and Evolution DOI: 10.1093/molbev/msz257
```

S-DIVA method:
```
Yu Y, Harris AJ, Blair C, He XJ. 2015. RASP (Reconstruct Ancestral State in Phylogenies): a tool for historical biogeography. Molecular Phylogenetics and Evolution 87:46-49  
Yu Y, Harris AJ, He XJ. 2010. S-DIVA (statistical dispersal-vicariance analysis): a tool for inferring biogeographic histories. Molecular Phylogenetics and Evolution. 56(2): 848-850  
```
DEC model (Lagrange):
```
Ree RH and SA Smith. 2008. Maximum likelihood inference of geographic range evolution by dispersal, local extinction, and cladogenesis. Systematic Biology. 57(1): 4-14.  
```
S-DEC method:
```
Yu Y, Harris AJ, Blair C, He XJ. 2015. RASP (Reconstruct Ancestral State in Phylogenies): a tool for historical biogeography. Molecular Phylogenetics and Evolution 87:46-49  
Beaulieu, J.M., Tank, D.C., Donoghue, M.J., 2013. A Southern Hemisphere origin for campanulid angiosperms, with traces of the break-up of Gondwana. BMC Evolutionary Biology. 13(1), 80.   
```
Bayarea:
```
Landis MJ, Matzke NJ, Moore BR, et al. 2013.Bayesian analysis of biogeography when the number of areas is large. Systematic biology. 62 (6): 789-804   
```
BBM:
```
Ali SS, Yu Y, Pfosser M, Wetschnig W. 2012. Inferences of biogeographical histories within subfamily Hyacinthoideae using S-DIVA and Bayesian binary MCMC analysis implemented in RASP (Reconstruct Ancestral State in Phylogenies). Ann Bot 109:95-107.
Ronquist F, Huelsenbeck JP (2003) MrBayes3: Bayesian phylogenetic inference undermixed models. Bioinformatics 19:1572–1574.  
```
BioGeoBEARS:
```
Matzke, Nicholas J. (2013). Probabilistic Historical Biogeography: New Models for Founder-Event Speciation, Imperfect Detection, and Fossils Allow Improved Accuracy and Model-Testing. Frontiers of Biogeography. 5(4), 242-248.   
Matzke, N.J. (2014). Model Selection in Historical Biogeography Reveals that Founder-event Speciation is a Crucial Process in Island Clades. Systematic Biology. 63(6): 951-970.  
Massana, Kathryn A.; Beaulieu, Jeremy M.; Matzke, Nicholas J.; O’Meara, Brian C. (2015). Non-null Effects of the Null Range in Biogeographic Models: Exploring Parameter Estimation in the DEC Model. bioRxiv, http://biorxiv.org/content/early/2015/09/16/026914  
Matzke, Nicholas J. (2013). BioGeoBEARS: BioGeography with Bayesian (and Likelihood) Evolutionary Analysis in R Scripts. R package, version 0.2.1, published July 27, 2013 at: http://CRAN.R-project.org/package=BioGeoBEARS  
R Core Team (2017) R: A language and environment for statistical computing. R Foundation for Statistical Computing, Vienna, Austria. Available at: https://www.R-project.org/  
```
BayesTraits:
```
Meade, A., & Pagel, M. (2018). BayesTraits: a computer package for analyses of trait evolution. available at http://www.evolution.rdg.ac.uk/BayesTraitsV3.0.1/BayesTraitsV3.0.1.html  
```
To use Ancestral reconstruction in R package 'ape' 5.3, please cite:
```
Paradis, E. and Schliep, K., 2018. ape 5.0: an environment for modern phylogenetics and evolutionary analyses in R. Bioinformatics, 35(3), pp.526-528.  
```
The triple distance (Steel M. 1992) is implemented in mp-est 2.0 (Liu, et al. 2010). The RF distance (Robinson and Foulds 1981), KF distance (Kuhner and Felsenstein 1994), path difference (Steel and Penny 1993) and SPR distance (de Oliveira Martins, et al. 2008; De Oliveira Martins, et al. 2014) were implemented in R package 'phangorn' 2.5.5 (Schliep 2010).  
Cites:  
```
Robinson, D.F. and Foulds,  L.R. (1981) Comparison of phylogenetic trees, Mathematical Biosciences, 53(1), 131-147  
De Oliveira Martins L., Leal E., Kishino H. (2008) Phylogenetic Detection of Recombination with a Bayesian Prior on the Distance between Trees. PLoS ONE 3(7). e2651. doi: 10.1371/journal.pone.0002651
De Oliveira Martins, L., Mallo, D., & Posada, D. (2014). A Bayesian supertree model for genome-wide species tree reconstruction. Systematic biology, 65(3), 397-416.
Kuhner, M. K. and Felsenstein, J. (1994) A simulation comparison of phylogeny algorithms under equal and unequal evolutionary rates, Molecular Biology and Evolution, 11(3), 459-468
Liu, L., Yu, L., and Edwards, S.V. (2010). A maximum pseudo-likelihood approach for estimating species trees under the coalescent model. BMC Evol. Biol. 10: 302
Schliep, K. P. (2010). phangorn: phylogenetic analysis in R. Bioinformatics, 27(4), 592-593.
Steel M. A. and Penny P. (1993) Distributions of tree comparison metrics - some new results, Syst. Biol., 42(2), 126-141
Steel M. (1992) The Complexity of Reconstructing Trees from Qualitative Characters and Subtrees. Journal of Classification 9:91-116.
```
For the Continuous states, Moran's I (Moran 1948, 1950), Abouheif's Cmean (Abouheif 1999), Pagel's λ (Pagel 1999) and Blomberg's K (Blomberg et al. 2003) models were calculate using R package 'adephylo' 1.1-11 (Jombart et al. 2010).  
For the discrete states, Pagel's λ is calculated using R package 'geiger' 2.0.6.2 (Pennell, et al. 2014).  
Cites:  
```
Abouheif, E. 1999. A method for testing the assumption of phylogenetic independence in comparative data. Evol. Ecol. Res. 1:895-909.  
Blomberg, S. P., Garland, T. and Ives, A. R. (2003) Testing for phylogenetic signal in comparative data: Behavioral traits are more labile. Evolution, 57, 717-745.  
Jombart T, Balloux F, Dray S. Adephylo: new tools for investigating the phylogenetic signal in biological traits. Bioinformatics, 2010, 26(15): 1907-1909.  
Moran, P. A. P. 1948. The interpretation of statistical maps. J. R. Stat. Soc. Ser. B Methodol. 10:243-251.  
Moran, P. A. P. 1950. Notes on continuous stochastic phenomena. Biometrika 37:17-23.  
Pagel, M. (1999) Inferring the historical patterns of biological evolution. Nature, 401, 877-884.  
Pennell, M. W., Eastman, J. M., Slater, G. J., et al. (2014). geiger v2. 0: an expanded suite of methods for fitting macroevolutionary models to phylogenetic trees. Bioinformatics, 30(15), 2216-2218.  
```